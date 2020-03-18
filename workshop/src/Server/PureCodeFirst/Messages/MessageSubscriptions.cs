using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace Chat.Server.Messages
{
    [ExtendObjectType(Name = "Subscription")]
    public class MessageSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<Message>> OnMessageReceivedAsync(
            [GlobalState]string currentUserEmail,
            [Service]ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Message>(
                currentUserEmail, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
