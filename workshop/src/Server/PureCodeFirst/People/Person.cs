using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using HotChocolate;

namespace Chat.Server.People
{
    public class Person
    {
        public Person(
            Guid id,
            Guid userId,
            string name,
            string email,
            DateTime lastSeen,
            Uri? imageUri,
            IReadOnlyList<Guid> friendIds)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Email = email;
            LastSeen = lastSeen;
            FriendIds = friendIds;
        }

        public Guid Id { get; }

        [GraphQLIgnore]
        public Guid UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public DateTime LastSeen { get; }

        public Uri? ImageUri { get; }

        [GraphQLIgnore]
        public IReadOnlyList<Guid> FriendIds { get; }

        public Person AddFriendId(Guid id)
        {
            if (FriendIds.Contains(id))
            {
                return this;
            }

            return new Person(
                Id,
                UserId,
                Name,
                Email,
                LastSeen,
                ImageUri,
                new List<Guid>(FriendIds) { id });
        }
    }
}
