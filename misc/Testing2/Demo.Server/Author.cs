using HotChocolate.Authorization;

namespace Demo.Server;

public class Author
{
    public required string Name { get; init; }

    [Authorize("Admin", ApplyPolicy.BeforeResolver)]
    public string? Address { get; init; }

    public string? SomeOtherField { get; init; }
}
