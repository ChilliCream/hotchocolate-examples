using HotChocolate.Types;

namespace HotChocolate.Examples.Paging
{
    public class UserType
        : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
        }
    }
}
