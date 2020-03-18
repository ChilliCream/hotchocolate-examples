using System;

#nullable disable

namespace Chat.Server.People
{
    public class PersonToFriend
    {
        public Guid PersionId { get; set; }
        public Person Person { get; set; }
        public Guid FriendId { get; set; }
        public Person Friend { get; set; }
    }
}
