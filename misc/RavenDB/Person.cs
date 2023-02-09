using HotChocolate.Types.Relay;

namespace raven;

public class Person
{
    [ID<Person>]
    public string Id { get; set; }

    public string Name { get; set; }

    public string[] Friends { get; set; }
}
