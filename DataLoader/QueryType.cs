using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace HotChocolate.Examples.Paging
{
    public class QueryType
        : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("messages")
                .UsePaging<MessageType>()
                .Resolver(ctx => ctx.Service<MessageRepository>().GetAllMessages());

            descriptor.Field("usersByCountry")
                .Argument("country", a => a.Type<NonNullType<StringType>>())
                .Type<NonNullType<ListType<NonNullType<UserType>>>>()
                .Resolver(ctx =>
                {
                    var userRepository = ctx.Service<UserRepository>();

                    IDataLoader<string, User[]> userDataLoader =
                        ctx.GroupedDataLoader<string, User>(
                            "usersByCountry",
                            k => userRepository.GetUsersByCountry(k, ctx.RequestAborted));

                    return userDataLoader.LoadAsync(ctx.Argument<string>("country"));
                });
        }
    }
}
