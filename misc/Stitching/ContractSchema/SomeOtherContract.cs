using System;

namespace Demo.Contracts
{
    public class SomeOtherContract
        : IContract
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
