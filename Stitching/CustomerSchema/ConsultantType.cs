using HotChocolate.Types;

namespace Demo.Customers
{
    public class ConsultantType
        : ObjectType<Consultant>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Consultant> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
        }
    }
}
