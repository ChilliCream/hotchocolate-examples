using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace Chat.Server.People
{
    [ExtendObjectType(Name = "Subscription")]
    public class PersonSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<Person>> OnOnlineAsync(
            [Service]ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, Person>(
                "online", cancellationToken)
                .ConfigureAwait(false);

        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<Person>> OnTypingAsync(
            [Service]ITopicEventReceiver eventReceiver,
            [GlobalState]string currentUserEmail,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, Person>(
                $"typing_to_{currentUserEmail}", cancellationToken)
                .ConfigureAwait(false);
    }
}