
using System;
using System.Collections.Generic;
using HotChocolate.Types.Relay;

namespace MongoDB
{
    [Node(
        NodeResolverType = typeof(PersonNodeResolver),
        NodeResolver = nameof(PersonNodeResolver.ResolveAsync))]
    public class Person
    {
        [ID]
        public Guid Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyList<Address> Addresses { get; init; }

        public Address MainAddress { get; init; }
    }
}