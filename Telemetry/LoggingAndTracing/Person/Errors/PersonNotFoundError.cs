using System;

namespace Telemetry;

public class PersonNotFoundError(Guid id) : Exception($"Person with id {id} not found")
{
}
