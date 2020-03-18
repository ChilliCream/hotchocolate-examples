using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server.Messages
{
    public interface IMessageRepository
    {
        IQueryable<Message> GetMessages(
            Guid senderId, 
            Guid recipientId);

        Task AddMessageAsync(
            Message message, 
            CancellationToken cancellationToken);
    }
}