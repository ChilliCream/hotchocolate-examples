using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace SlackClone.Models
{
    public class User
    {
        [Key]
        [GraphQLNonNullType]
        public string Email { get; set; }
        [GraphQLNonNullType]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [GraphQLIgnore]
        public string PasswordHash { get; set; }
        [GraphQLIgnore]
        public string Salt { get; set; }
        public bool Online { get; set; }
        public DateTime LastSeen { get; set; }

        public List<ChannelMember> Channels { get; set; }
    }
}
