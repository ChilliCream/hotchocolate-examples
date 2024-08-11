using System;
using HotChocolate.Types.Relay;

namespace Telemetry;

public sealed class Person(string name)
{
    [ID]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; } = name;
}