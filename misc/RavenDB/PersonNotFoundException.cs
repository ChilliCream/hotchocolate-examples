using System;
using System.Globalization;
using HotChocolate.Types.Relay;

namespace raven;

public class PersonNotFoundException : Exception
{
    private const string _message = "Could not find person with id {0}";

    public PersonNotFoundException(string id)
        : base(string.Format(CultureInfo.InvariantCulture, _message, id))
    {
        Id = id;
    }

    [ID<Person>]
    public string Id { get; }
}
