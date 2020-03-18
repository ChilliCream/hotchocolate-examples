using HotChocolate.Resolvers;
using HotChocolate.Types;
using GreenDonut;
using MongoDB.Bson;

namespace HotChocolate.Examples.Paging
{
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

                IDataLoader<ObjectId, User> dataLoader = ctx.BatchDataLoader<ObjectId, User>(
                    "UserById",
                    repository.GetUsersAsync);

                return dataLoader.LoadAsync(ctx.Parent<Message>().UserId);
            });
            descriptor.Field("replyTo").Type<MessageType>().Resolver(async ctx =>
            {
                ObjectId? replyToId = ctx.Parent<Message>().ReplyToId;
                if (replyToId.HasValue)
                {
                    MessageRepository repository = ctx.Service<MessageRepository>();

                    IDataLoader<ObjectId, Message> dataLoader = ctx.CacheDataLoader<ObjectId, Message>(
                        "MessageById",
                        repository.GetMessageById);

                    return await dataLoader.LoadAsync(ctx.Parent<Message>().ReplyToId.Value);
                }
                return null;
            });
            descriptor.Ignore(t => t.UserId);
            descriptor.Ignore(t => t.ReplyToId);
        }
    }
}
