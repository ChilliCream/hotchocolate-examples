using HotChocolate.Types;

namespace Demo.Customers
{
    public class CustomerOrConsultantType
        : UnionType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name("CustomerOrConsultant");
            descriptor.Type<CustomerType>();
            descriptor.Type<ConsultantType>();
        }
    }
}
