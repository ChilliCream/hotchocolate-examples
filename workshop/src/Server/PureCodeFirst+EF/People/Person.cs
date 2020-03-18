using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;
using HotChocolate.Types;

#nullable disable

namespace Chat.Server.People
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [GraphQLIgnore]
        public Guid UserId { get; set; }

        [GraphQLNonNullType]
        public string Name { get; set; }

        [GraphQLNonNullType]
        public string Email { get; set; }

        public DateTime LastSeen { get; set; }

        public Uri ImageUri { get; set; }

        [GraphQLIgnore]
        public List<PersonToFriend> Friends { get; } = new List<PersonToFriend>();

        [GraphQLIgnore]
        public List<PersonToFriend> FriendOf { get; } = new List<PersonToFriend>();
    }
}
