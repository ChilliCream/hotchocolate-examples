using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            [Service]ChatDbContext dbContext,
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
                await dbContext.People.Where(t =>
                    t.Email == input.Email || t.Email == currentUserEmail)
                    .ToArrayAsync(cancellationToken);

            if (people.Count < 1)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The provided friend email address is invalid.")
                        .SetCode("EMAIL_UNKNOWN")
                        .Build());
            }

            people[0].Friends.Add(new PersonToFriend
            {
                PersionId = people[0].Id,
                FriendId = people[1].Id
            });

            people[1].Friends.Add(new PersonToFriend
            {
                PersionId = people[1].Id,
                FriendId = people[0].Id
            });

            await dbContext.SaveChangesAsync(cancellationToken);

            return new InviteFriendPayload(
                people[1],
                input.ClientMutationId);
        }

        public async Task<TypingPayload> TypingAsync(
            TypingInput input,
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            IReadOnlyList<Person> participants =
                await personByEmail.LoadAsync(
                        cancellationToken, input.WritingTo, currentUserEmail);

            await eventSender.SendAsync(
                $"typing_to_{participants[0].Email}", participants[1], cancellationToken);

            return new TypingPayload(participants[0], participants[1], input.ClientMutationId);
        }
    }
}