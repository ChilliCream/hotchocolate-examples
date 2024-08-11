using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Telemetry;

public static partial class PersonOperations
{
    [Query]
    [UsePaging]
    public static IEnumerable<Person> GetPeople(PersonService service)
    {
        return service.GetPeople();
    }

    [Mutation]
    [Error<PersonAlreadyExistsError>]
    [Error<NameTooShortError>]
    public static Person CreatePerson(PersonService service, string name)
    {
        return service.CreatePerson(name);
    }

    [Mutation]
    [Error<PersonNotFoundError>]
    [Error<NameTooShortError>]
    [Error<PersonAlreadyExistsError>]
    public static Person? UpdatePersonById(
        PersonService service,
        [ID<Person>] Guid id,
        string name)
    {
        return service.UpdatePersonById(id, name);
    }
}