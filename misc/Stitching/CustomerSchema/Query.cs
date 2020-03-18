using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Types.Relay;

namespace Demo.Customers
{
    public class Query
    {
        private readonly IdSerializer _idSerializer = new IdSerializer();
        private readonly CustomerRepository _repository;

        public Query(CustomerRepository repository)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public Customer GetCustomer(string id)
        {
            IdValue value = _idSerializer.Deserialize(id);
            return _repository.Customers
                .FirstOrDefault(t => t.Id.Equals(value.Value));
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _repository.Customers;
        }

        public Consultant GetConsultant(string id)
        {
            IdValue value = _idSerializer.Deserialize(id);
            return _repository.Consultants
                .FirstOrDefault(t => t.Id.Equals(value.Value));
        }

        public ICustomerOrConsultant GetCustomerOrConsultant(string id)
        {
            IdValue value = _idSerializer.Deserialize(id);
            if (value.TypeName == "Consultant")
            {
                return GetConsultant(id);
            }
            return GetCustomer(id);
        }
    }
}
