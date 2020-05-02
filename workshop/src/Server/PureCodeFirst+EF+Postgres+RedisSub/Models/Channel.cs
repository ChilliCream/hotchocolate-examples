using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace SlackClone.Models
{
    public class Channel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ChannelMember> Members { get; set; }
        public List<ChannelMessage> Messages { get; set; }

        [ForeignKey("User"), GraphQLIgnore]
        public string CreatedByEmail { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}