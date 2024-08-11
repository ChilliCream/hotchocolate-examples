using System;

namespace Telemetry;

public class NameTooShortError(string name)
    : Exception($"Name must be at least 3 characters long but was {name}")
{
}
