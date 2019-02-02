using MongoDB.Bson;

namespace HotChocolate.Examples.Paging
{
    public class MessageInput
    {
        public string Text { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId? ReplyToId { get; set; }
    }
}
