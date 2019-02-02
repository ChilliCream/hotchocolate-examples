using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.Examples.Paging
{
    public class Mutation
    {
        public async Task<Message> CreateMessageAsync(
            MessageInput messageInput,
            [Service]MessageRepository repository,
            CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Text = messageInput.Text,
                UserId = messageInput.UserId,
                ReplyToId = messageInput.ReplyToId,
                Created = DateTimeOffset.UtcNow,
            };

            await repository.CreateMessageAsync(message, cancellationToken);

            return message;
        }

        public async Task<User> CreateUserAsync(
            UserInput userInput,
            [Service]UserRepository repository,
            CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = userInput.Name,
                Country = userInput.Country
            };

            await repository.CreateUserAsync(user, cancellationToken);

            return user;
        }
    }
}
