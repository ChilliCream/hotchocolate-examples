using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Server.Messages;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Chat.Server.People
{
    [ExtendObjectType(Name = "Person")]
    public class PersonExtension
    {
        public bool IsOnline([Parent]Person person) => 
            person.LastSeen < DateTime.UtcNow.AddMinutes(10);

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Message> GetMessages(
            [GlobalState]Guid currentPersonId,
            [Parent]Person recipient,
            [Service]IMessageRepository repository,
            CancellationToken cancellationToken)
        {
            return repository.GetMessages(currentPersonId, recipient.Id);
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Person>> GetFriendsAsync(
            [Parent]Person recipient,
            PersonByIdDataLoader personById,
            CancellationToken cancellationToken)
        {
            return await personById.LoadAsync(
                recipient.FriendIds, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
