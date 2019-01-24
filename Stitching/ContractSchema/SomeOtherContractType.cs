using HotChocolate.Types;

namespace Demo.Contracts
{
    public class SomeOtherContractType
        : ObjectType<SomeOtherContract>
    {
        protected override void Configure(
            IObjectTypeDescriptor<SomeOtherContract> descriptor)
        {
            descriptor.Interface<ContractType>();

            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field(t => t.CustomerId)
                .Ignore();

            descriptor.Field(t => t.ExpiryDate)
                .Type<NonNullType<DateTimeType>>();
        }
    }
}
