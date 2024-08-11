using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telemetry;

/// <summary>
/// This service generates some logs
/// </summary>
public sealed class PersonPostProcessor(ILogger<PersonPostProcessor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Process();
            await Task.Delay(500, stoppingToken);
        }
    }

    private void Process()
    {
        using var activity = App.Trace.StartActivity("Person Post Processing");

        var random = Random.Shared.NextInt64(0, 10);

        switch (random)
        {
            case 0:
                logger.LogWarning("Post-processing of the person encountered issues.");
                break;
            case 1:
                logger.LogError("Post-processing of the person failed due to an unexpected error.");
                break;
            case 2:
                logger.LogCritical("Post-processing of the person failed catastrophically!");
                break;
            case 3:
                logger.LogInformation("Post-processing of the person completed successfully.");
                break;
            case 4:
                logger.LogInformation("The person has already been processed previously.");
                break;
            case 5:
                logger.LogDebug("Person 123 processing completed with debug-level details.");
                break;
            case 6:
                logger.LogTrace("Detailed trace for processing Person 123.");
                break;
            case 7:
                logger.LogWarning("Processing of Person 123 completed with minor issues.");
                break;
            case 8:
                logger.LogError("Processing of Person 123 encountered errors.");
                break;
            case 9:
                logger.LogCritical("Processing of Person 123 failed critically!");
                break;
            default:
                logger.LogInformation("Post-processing of the person completed successfully.");
                break;
        }
    }
}