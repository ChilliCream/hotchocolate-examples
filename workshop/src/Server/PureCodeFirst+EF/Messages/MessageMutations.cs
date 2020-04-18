using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chat.Server.People;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Language;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace Chat.Server.Messages
{
    [ExtendObjectType(Name = "Mutation")]
    public class MessageMutations
    {
        public async Task<SendMessagePayload> SendMessageAsync(
            SendMessageInput input,
            FieldNode field,
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            [Service]ChatDbContext dbContext,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            IReadOnlyList<Person> participants =
                await personByEmail.LoadAsync(
                    cancellationToken, currentUserEmail, input.RecipientEmail);

            if (participants[1] is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetCode("UNKNOWN_RECIPIENT")
                        .SetMessage("The given recipient id is invalid.")
                        .AddLocation(field.Arguments[0])
                        .Build());
            }

            Person sender = participants[0];
            Person recipient = participants[1];

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Text = input.Text,
                Sent = DateTime.UtcNow
            };

            await dbContext.Messages.AddAsync(message, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(recipient.Email, message, cancellationToken);

            return new SendMessagePayload(
                sender,
                recipient,
                message,
                input.ClientMutationId);
        }
    }
}
