using System;
using HotChocolate;

namespace Chat.Server.Users
{
    public class User
    {
        public User(
            Guid id,
            Guid personId,
            string email,
            string passwordHash,
            string salt)
        {
            Id = id;
            PersonId = personId;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        public Guid Id { get; }

        public Guid PersonId { get; }

        public string Email { get; }

        [GraphQLIgnore]
        public string PasswordHash { get; }

        [GraphQLIgnore]
        public string Salt { get; }
    }
}
