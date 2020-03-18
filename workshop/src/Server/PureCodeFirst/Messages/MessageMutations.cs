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
            [Service]IMessageRepository messageRepository,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            IReadOnlyList<Person> participants =
                await personByEmail.LoadAsync(
                    cancellationToken, currentUserEmail, input.RecipientEmail)
                    .ConfigureAwait(false);

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

            var message = new Message(sender.Id, recipient.Id, input.Text);

            await messageRepository.AddMessageAsync(
                message, cancellationToken)
                .ConfigureAwait(false);

            await eventSender.SendAsync(
                recipient.Email, message, cancellationToken)
                .ConfigureAwait(false);

            return new SendMessagePayload(
                sender,
                recipient,
                message,
                input.ClientMutationId);
        }
    }
}
