using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace Chat.Server.People
{
    [ExtendObjectType(Name = "Mutation")]
    public class PersonMutations
    {
        public async Task<InviteFriendPayload> InviteFriendAsync(
            InviteFriendInput input,
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            [Service]IPersonRepository personRepository,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email address cannot be empty.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            IReadOnlyList<Person> people =
                await personByEmail.LoadAsync(
                    cancellationToken, input.Email, currentUserEmail)
                    .ConfigureAwait(false);

            if (people[0] is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The provided friend email address is invalid.")
                        .SetCode("EMAIL_UNKNOWN")
                        .Build());
            }

            await personRepository.AddFriendIdAsync(
                people[1].Id, people[0].Id, cancellationToken)
                .ConfigureAwait(false);

            await personRepository.AddFriendIdAsync(
                people[0].Id, people[1].Id, cancellationToken)
                .ConfigureAwait(false);

            return new InviteFriendPayload(
                people[1].AddFriendId(people[0].Id),
                input.ClientMutationId);
        }

        public async Task<TypingPayload> TypingAsync(
            TypingInput input,
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            IReadOnlyList<Person> participants = await personByEmail.LoadAsync(
                cancellationToken, input.WritingTo, currentUserEmail)
                .ConfigureAwait(false);

            await eventSender.SendAsync(
                $"typing_to_{participants[0].Email}", participants[1], cancellationToken)
                .ConfigureAwait(false);

            return new TypingPayload(participants[0], participants[1], input.ClientMutationId);
        }
    }
}