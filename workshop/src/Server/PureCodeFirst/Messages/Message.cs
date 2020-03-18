using System;
using HotChocolate;

namespace Chat.Server.Messages
{
    public class Message
    {
        public Message(
            Guid senderId,
            Guid recipientId,
            string text)
            : this(Guid.NewGuid(), senderId, recipientId, text, DateTime.UtcNow)
        {
        }

        public Message(
            Guid id,
            Guid senderId,
            Guid recipientId,
            string text,
            DateTime sent)
        {
            Id = id;
            SenderId = senderId;
            RecipientId = recipientId;
            Text = text;
            Sent = sent;
        }

        public Guid Id { get; }

        [GraphQLIgnore]
        public Guid SenderId { get; }

        [GraphQLIgnore]
        public Guid RecipientId { get; }

        public string Text { get; }

        public DateTime Sent { get; }
    }
}
