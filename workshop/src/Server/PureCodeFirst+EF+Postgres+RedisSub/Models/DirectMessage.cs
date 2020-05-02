using System;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace SlackClone.Models
{
    public class DirectMessage
    {
        public Guid Id { get; set; }

        [ForeignKey("User"), GraphQLIgnore]
        public Guid SenderId { get; set; }
        public virtual User Sender { get; }

        [ForeignKey("User"), GraphQLIgnore]
        public Guid RecipientId { get; set; }
        public virtual User Recipient { get; }

        [GraphQLNonNullType]
        public string Text { get; set; }

        public DateTime SentAtUTC { get; set; }
    }
}
