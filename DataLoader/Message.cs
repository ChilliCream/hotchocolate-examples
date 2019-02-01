using System;
using MongoDB.Bson;
using HotChocolate.Language;

namespace HotChocolate.Examples.Paging
{
    public class Message
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Favorites { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId? ReplyToId { get; set; }
    }
}
