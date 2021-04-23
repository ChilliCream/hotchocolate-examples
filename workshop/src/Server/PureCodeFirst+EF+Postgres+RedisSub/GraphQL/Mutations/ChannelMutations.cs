using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using SlackClone.Models;

namespace SlackClone.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class ChannelMutations
    {
        [Authorize]
        public async Task<CreateMutationResponse<Channel>> CreateChannel(
            CreateChannelInput input,
            [GlobalState]string currentUserEmail,
            [Service]SlackCloneDbContext dbContext,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(input.Name))
                {
                    throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The channel name can not be null.")
                        .SetCode("INVALID_INPUT")
                        .Build());
                }

                var channel = new Channel()
                {
                    Name = input.Name,
                    Description = input.Description,
                    CreatedAt = DateTime.UtcNow,
                    CreatedByEmail = currentUserEmail
                };

                dbContext.Channels.Add(channel);
                await dbContext.SaveChangesAsync(cancellationToken);
                bool ok = true;

                await eventSender.SendAsync("channelCreated", channel, cancellationToken).ConfigureAwait(false);

                return new CreateMutationResponse<Channel>(ok, channel);
            }
            catch (DbUpdateException e)
            {
                throw new QueryException($"DbUpdateException error details - {e?.InnerException?.Message}");
            }
        }

        [Authorize]
        public async Task<AddMessageToChannelResponse> AddMessageToChannel(
            AddMessageToChannelInput input,
            [GlobalState]string currentUserEmail,
            [Service]SlackCloneDbContext dbContext,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {

            var message = new ChannelMessage
            {
                Id = Guid.NewGuid(),
                ChannelId = input.ChannelId,
                Text = input.Text,
                CreatedAtUTC = DateTime.UtcNow,
                CreatedByEmail = currentUserEmail,
                Likes = 0
            };

            dbContext.ChannelMessages.Add(message);
            await dbContext.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(input.ChannelId, message, cancellationToken).ConfigureAwait(false);

            return new AddMessageToChannelResponse(true);
        }

    }
}
