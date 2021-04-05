
using System;
using System.Collections.Generic;
using HotChocolate.Types.Relay;

namespace MongoDB
{
    [Node(
        IdField = nameof(Id),
        NodeResolverType = typeof(PersonNodeResolver),
        NodeResolver = nameof(PersonNodeResolver.ResolveAsync))]
    public class Person
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyList<Address> Addresses { get; init; }

        public Address MainAddress { get; init; }
    }
}