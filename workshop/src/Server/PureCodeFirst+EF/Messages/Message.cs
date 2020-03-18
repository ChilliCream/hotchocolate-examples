using System;
using Chat.Server.People;
using HotChocolate;

#nullable disable

namespace Chat.Server.Messages
{
    public class Message
    {
        public Guid Id { get; set; }

        [GraphQLIgnore]
        public Guid SenderId { get; set; }

        [GraphQLIgnore]
        public Guid RecipientId { get; set; }

        [GraphQLNonNullType]
        public string Text { get; set; }

        public DateTime Sent { get; set; }
    }
}
