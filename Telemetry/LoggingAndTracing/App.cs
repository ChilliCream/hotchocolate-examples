using System.Diagnostics;

namespace Telemetry;

public static class App
{
    public static readonly ActivitySource Trace = new("Telemetry");
}