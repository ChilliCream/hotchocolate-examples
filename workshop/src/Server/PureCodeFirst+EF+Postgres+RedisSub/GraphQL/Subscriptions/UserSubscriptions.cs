using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using SlackClone.Models;

namespace SlackClone.GraphQL.Subscriptions
{

    [ExtendObjectType(Name = "Subscription")]
    public class UserSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<User>> OnOnline(
            [Service]ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, User>("online", cancellationToken);

        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<User>> OnTypingAsync(
            [Service]ITopicEventReceiver eventReceiver,
            [GlobalState]string currentUserEmail,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, User>(
                $"typing_to_{currentUserEmail}", cancellationToken);
    }

}
