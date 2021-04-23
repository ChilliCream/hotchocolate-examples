using System;
using System.ComponentModel.DataAnnotations;

namespace SlackClone.Models
{
    public class ChannelMember
    {
        [Key]
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        [Key]
        public string MemberEmail { get; set; }
        public User Member { get; set; }
    }
}
