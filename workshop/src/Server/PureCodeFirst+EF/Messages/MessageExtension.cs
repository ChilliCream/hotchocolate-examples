using System.Threading;
using System.Threading.Tasks;
using Chat.Server.People;
using HotChocolate;
using HotChocolate.Types;

namespace Chat.Server.Messages
{
    [ExtendObjectType(Name = "Message")]
    public class MessageExtension
    {
        public async Task<Direction> GetDirectionAsync(
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            [Parent]Message message,
            CancellationToken cancellationToken)
        {
            Person sender = await personByEmail.LoadAsync(currentUserEmail, cancellationToken);

            if (message.RecipientId == message.SenderId
                && message.SenderId == sender.Id)
            {
                return Direction.Incoming;
            }

            if (message.SenderId == sender.Id)
            {
                return Direction.Outgoing;
            }

            return Direction.Incoming;
        }

        public async Task<Person> GetSenderAsync(
            [Parent]Message message,
            PersonByIdDataLoader personById,
            CancellationToken cancellationToken)
        {
            return await personById.LoadAsync(message.SenderId, cancellationToken);
        }

        public async Task<Person> GetRecipientAsync(
            [Parent]Message message,
            PersonByIdDataLoader personById,
            CancellationToken cancellationToken)
        {
            return await personById.LoadAsync(message.RecipientId, cancellationToken);
        }
    }
}
