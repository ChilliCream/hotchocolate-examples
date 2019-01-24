using HotChocolate.Types;

namespace Demo.Customers
{
    public class QueryType
        : ObjectType<Query>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.GetCustomer(default))
                .Argument("id", a => a.Type<NonNullType<IdType>>())
                .Type<CustomerType>();

            descriptor.Field(t => t.GetCustomers())
                .Type<NonNullType<ListType<NonNullType<CustomerType>>>>();

            descriptor.Field(t => t.GetConsultant(default))
                .Argument("id", a => a.Type<NonNullType<IdType>>())
                .Type<ConsultantType>();

            descriptor.Field(t => t.GetCustomerOrConsultant(default))
                .Argument("id", a => a.Type<NonNullType<IdType>>())
                .Type<CustomerOrConsultantType>();
        }
    }
}
