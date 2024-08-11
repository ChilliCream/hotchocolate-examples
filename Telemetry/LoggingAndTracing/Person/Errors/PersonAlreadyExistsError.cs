using System;

namespace Telemetry;

public class PersonAlreadyExistsError(string name)
    : Exception($"Person with name {name} already exists")
{
}
