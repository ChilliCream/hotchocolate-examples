using HotChocolate.Types;

namespace HotChocolate.Examples.Paging
{
    public class UserInputType
        : InputObjectType<UserInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserInput> descriptor)
        {
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Country).Type<NonNullType<StringType>>();
        }
    }
}
