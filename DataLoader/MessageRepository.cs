using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HotChocolate.Examples.Paging
{
    public class MessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(IMongoCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection
                ?? throw new ArgumentNullException(nameof(messageCollection));
        }

        public IQueryable<Message> GetAllMessages()
        {
            return _messageCollection.AsQueryable();
        }

        public Task<Message> GetMessageById(ObjectId messageId)
        {
            return _messageCollection.AsQueryable().FirstOrDefaultAsync(t => t.Id == messageId);
        }

        public Task CreateMessageAsync(Message message, CancellationToken cancellationToken)
        {
            return _messageCollection.InsertOneAsync(message, new InsertOneOptions(), cancellationToken);
        }
    }
}
