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
    public class ChannelSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<Channel>> OnCreateChannel(
            [Service]ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Channel>(
                "channelCreated", cancellationToken).ConfigureAwait(false);
        }

        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<ChannelMessage>> OnChannelMessageAdd(
            Guid channelId,
            [Service]ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<Guid, ChannelMessage>(
                channelId, cancellationToken).ConfigureAwait(false);
        }
    }
}
