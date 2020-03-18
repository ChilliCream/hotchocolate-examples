using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Chat.Server.People;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace Chat.Server.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        public async Task<CreateUserPayload> CreateUser(
            CreateUserInput input,
            [Service]ChatDbContext dbContext,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The name cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email cannot be empty.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password cannot be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            string salt = Guid.NewGuid().ToString("N");

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));

            Guid personId = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                PersonId = personId,
                Email = input.Email,
                PasswordHash = Convert.ToBase64String(hash),
                Salt = salt
            };

            var person = new Person
            {
                Id = personId,
                UserId = user.Id,
                Name = input.Name,
                Email = input.Email,
                LastSeen = DateTime.UtcNow,
                ImageUri = input.Image
            };

            dbContext.Users.Add(user);
            dbContext.People.Add(person);

            await dbContext.SaveChangesAsync();

            return new CreateUserPayload(user, input.ClientMutationId);
        }

        public async Task<LoginPayload> LoginAsync(
            LoginInput input,
            [Service]ChatDbContext dbContext,
            [Service]PersonByEmailDataLoader personByEmail,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email mustn't be empty.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password mustn't be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            User? user = await dbContext.Users.FirstOrDefaultAsync(t => t.Email == input.Email);

            if (user is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));

            if (!Convert.ToBase64String(hash).Equals(user.PasswordHash, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            Person me = await personByEmail.LoadAsync(input.Email, cancellationToken);

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(WellKnownClaimTypes.UserId, me.Id.ToString()),
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

            await eventSender.SendAsync<string, Person>("online", me);

            return new LoginPayload(me, tokenString, "bearer", input.ClientMutationId);
        }
    }
}
