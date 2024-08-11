using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Telemetry;

public class PersonService(ILogger<PersonService> logger)
{
    private readonly List<Person> _people = new();

    public IEnumerable<Person> GetPeople()
    {
        return _people;
    }

    public Person CreatePerson(string name)
    {
        using var scope = App.Trace.StartActivity("Creating Person");

        var person = new Person(name);

        if (_people.Any(p => p.Name == name))
        {
            throw new PersonAlreadyExistsError(name);
        }

        if (name.Length < 3)
        {
            throw new NameTooShortError(name);
        }

        logger.PersonCreated(person.Name, person.Id);
        _people.Add(person);
        return person;
    }

    public Person? UpdatePersonById(Guid id, string name)
    {
        using var scope = App.Trace.StartActivity("Updating person");
        scope?.AddTag("person.id", id.ToString());

        var person = _people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            logger.PersonNotFound(id);
            throw new PersonNotFoundError(id);
        }

        if (name.Length < 3)
        {
            logger.NameTooShort(id);
            throw new NameTooShortError(name);
        }

        if (_people.Any(p => p.Name == name))
        {
            throw new PersonAlreadyExistsError(name);
        }

        return person;
    }
}

public static partial class Logs
{
    [LoggerMessage(LogLevel.Information, "Person {Name} created with id {Id}")]
    public static partial void PersonCreated(this ILogger logger, string name, Guid id);

    [LoggerMessage(LogLevel.Information, "Person {Id} updated with name {Name}")]
    public static partial void PersonUpdated(this ILogger logger, Guid id, string name);

    [LoggerMessage(LogLevel.Error, "Name of person with id {Id} is too short")]
    public static partial void NameTooShort(this ILogger logger, Guid id);

    [LoggerMessage(LogLevel.Error, "Person with id {Id} not found")]
    public static partial void PersonNotFound(this ILogger logger, Guid id);

    [LoggerMessage(LogLevel.Warning, "Person with name {Name} already exists")]
    public static partial void PersonAlreadyExists(this ILogger logger, string name);
}