using HotChocolate.Types;

namespace HotChocolate.Examples.Paging
{
    public class MessageInputType
        : InputObjectType<MessageInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MessageInput> descriptor)
        {
            descriptor.Field(t => t.Text).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.UserId).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.ReplyToId).Type<IdType>();
        }
    }
}
