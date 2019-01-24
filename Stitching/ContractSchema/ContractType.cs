using HotChocolate.Types;

namespace Demo.Contracts
{
    public class ContractType
        : InterfaceType
    {
        protected override void Configure(IInterfaceTypeDescriptor descriptor)
        {
            descriptor.Name("Contract");
            descriptor.Field("id").Type<NonNullType<IdType>>();
        }
    }
}
