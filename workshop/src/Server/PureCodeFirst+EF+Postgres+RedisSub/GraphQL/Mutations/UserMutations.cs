using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SlackClone.Models;

namespace SlackClone.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        public async Task<CreateMutationResponse<User>> SignUp(
            SignupInput input,
            [Service]SlackCloneDbContext dbContext,
            CancellationToken cancellationToken)
        {
            try
            {
                string salt = Guid.NewGuid().ToString("N");

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));

                var user = new User
                {
                    Email = input.Email,
                    PasswordHash = Convert.ToBase64String(hash),
                    Salt = salt,
                    DisplayName = input.DisplayName
                };

                dbContext.Users.Add(user);

                await dbContext.SaveChangesAsync(cancellationToken);
                return new CreateMutationResponse<User>(true, user);
            }
            catch (DbUpdateException e)
            {
                throw new QueryException($"DbUpdateException error details - {e?.InnerException?.Message}");
            }
        }

        public async Task<LoginResponse> Login(
            LoginInput input,
            [Service]SlackCloneDbContext dbContext,
            CancellationToken cancellationToken
        )
        {
            User user = await dbContext.Users.FirstOrDefaultAsync(t => t.Email == input.Email);

            if (user is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The user does not exist, please sign up.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));

            if (!Convert.ToBase64String(hash).Equals(user.PasswordHash, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified password is invalid.")
                        .SetCode("INVALID_PASSWORD")
                        .Build());
            }

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email)
            });

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Startup.SharedSecret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return new LoginResponse(user, tokenString);
        }
    }
}