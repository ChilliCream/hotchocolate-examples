using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Chat.Server.Messages
{
    public class MessageRepository
        : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(IMongoCollection<Message> messages)
        {
            _messages = messages;

            _messages.Indexes.CreateOne(
                new CreateIndexModel<Message>(
                    Builders<Message>.IndexKeys.Ascending(x => x.SenderId),
                    new CreateIndexOptions { Unique = false }));

            _messages.Indexes.CreateOne(
                new CreateIndexModel<Message>(
                    Builders<Message>.IndexKeys.Ascending(x => x.RecipientId),
                    new CreateIndexOptions { Unique = false }));
        }

        public IQueryable<Message> GetMessages(
            Guid senderId,
            Guid recipientId)
        {
            return _messages.AsQueryable().Where(t =>
                (t.SenderId == senderId && t.RecipientId == recipientId)
                || (t.RecipientId == senderId && t.SenderId == recipientId));
        }

        public async Task AddMessageAsync(
            Message message,
            CancellationToken cancellationToken)
        {
            await _messages.InsertOneAsync(
                message,
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }
    }
}