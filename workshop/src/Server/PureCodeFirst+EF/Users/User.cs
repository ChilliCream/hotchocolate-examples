using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Chat.Server.People;
using HotChocolate;

#nullable disable

namespace Chat.Server.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [GraphQLIgnore]
        public Guid PersonId { get; set; }

        [GraphQLNonNullType]
        public Person Person { get; set; }

        [GraphQLNonNullType]
        public string Email { get; set; }

        [GraphQLIgnore]
        public string PasswordHash { get; set; }

        [GraphQLIgnore]
        public string Salt { get; set; }
    }
}
