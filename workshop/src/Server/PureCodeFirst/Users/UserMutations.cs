using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            [Service]IUserRepository userRepository,
            [Service]IPersonRepository personRepository,
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

            var user = new User(
                Guid.NewGuid(),
                personId,
                input.Email,
                Convert.ToBase64String(hash),
                salt);

            var person = new Person(
                personId,
                user.Id,
                input.Name,
                input.Email,
                DateTime.UtcNow,
                input.Image,
                Array.Empty<Guid>());

            await userRepository.AddUserAsync(
                user, cancellationToken)
                .ConfigureAwait(false);

            await personRepository.AddPersonAsync(
                person, cancellationToken)
                .ConfigureAwait(false);

            return new CreateUserPayload(user, input.ClientMutationId);
        }

        public async Task<LoginPayload> LoginAsync(
            LoginInput input,
            [Service]IUserRepository userRepository,
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

            User? user = await userRepository.GetUserAsync(
                input.Email, cancellationToken)
                .ConfigureAwait(false);

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

            Person me = await personByEmail.LoadAsync(
                input.Email, cancellationToken)
                .ConfigureAwait(false);

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
