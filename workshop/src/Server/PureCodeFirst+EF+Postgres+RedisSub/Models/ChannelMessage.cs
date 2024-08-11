using System;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace SlackClone.Models
{
    public class ChannelMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public int Likes { get; set; }
        public Guid? ThreadId { get; set; }

        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        [ForeignKey("User"), GraphQLIgnore]
        public string CreatedByEmail { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
