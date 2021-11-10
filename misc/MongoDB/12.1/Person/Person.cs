
using System;
using System.Collections.Generic;

namespace Demo;

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
