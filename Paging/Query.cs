using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using GreenDonut;
using MongoDB.Bson;
using MongoDB.Driver;
using HotChocolate.Language;

namespace HotChocolate.Examples.Paging
{
    public class Message
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Favorites { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    public class MessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(IMongoCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection
                ?? throw new ArgumentNullException(nameof(messageCollection));
        }

        public IQueryable<Message> GetAllMessages()
        {
            return _messageCollection.AsQueryable();
        }

        public Task CreateMessageAsync(Message message, CancellationToken cancellationToken)
        {
            return _messageCollection.InsertOneAsync(message, new InsertOneOptions(), cancellationToken);
        }

    }

    public class UserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoCollection<User> userCollection)
        {
            _userCollection = userCollection
                ?? throw new ArgumentNullException(nameof(userCollection));
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userCollection.AsQueryable();
        }

        public Task<User> GetUserAsync(ObjectId userId, CancellationToken cancellationToken)
        {
            return _userCollection.Find(c => c.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            return _userCollection.InsertOneAsync(user, new InsertOneOptions(), cancellationToken);
        }

        public async Task<IReadOnlyDictionary<ObjectId, User>> GetUsersAsync(
            IReadOnlyCollection<ObjectId> userIds,
            CancellationToken cancellationToken)
        {
            var filters = new List<FilterDefinition<User>>();
            foreach (ObjectId userId in userIds)
            {
                filters.Add(Builders<User>.Filter.Eq(u => u.Id, userId));
            }

            List<User> users = await _userCollection
                .Find(Builders<User>.Filter.Or(filters))
                .ToListAsync(cancellationToken);

            return users.ToDictionary(t => t.Id);
        }
    }

    public class MessageType
        : ObjectType<Message>
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Text).Type<NonNullType<StringType>>();
            descriptor.Field("createdBy").Type<NonNullType<UserType>>().Resolver(ctx =>
            {
                UserRepository repository = ctx.Service<UserRepository>();

                IDataLoader<ObjectId, User> dataLoader = ctx.DataLoader<ObjectId, User>(
                    "UserById",
                    keys => repository.GetUsersAsync(keys, ctx.RequestAborted));

                return dataLoader.LoadAsync(ctx.Parent<Message>().UserId);
            });
            descriptor.Ignore(t => t.UserId);
        }
    }

    public class UserType
        : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
        }
    }

    public class QueryType
        : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("messages")
                .UsePaging<MessageType>()
                .Resolver(ctx => ctx.Service<MessageRepository>().GetAllMessages());
        }
    }

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
                Created = DateTimeOffset.UtcNow,
            };

            await repository.CreateMessageAsync(message, cancellationToken);

            return message;
        }

        public async Task<User> CreateUserAsync(
            string userName,
            [Service]UserRepository repository,
            CancellationToken cancellationToken)
        {
            var user = new User { Name = userName };
            await repository.CreateUserAsync(user, cancellationToken);
            return user;
        }
    }

    public class MessageInput
    {
        public string Text { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class MessageInputType
        : InputObjectType<MessageInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MessageInput> descriptor)
        {
            descriptor.Field(t => t.Text).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.UserId).Type<NonNullType<IdType>>();
        }
    }

    public class MutationType
        : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(t => t.CreateMessageAsync(default, default, default))
                .Argument("messageInput", a => a.Type<NonNullType<MessageInputType>>())
                .Type<MessageType>();

            descriptor.Field(t => t.CreateUserAsync(default, default, default))
                .Type<UserType>();
        }
    }
}
