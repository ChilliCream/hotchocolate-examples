using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Log;
using OpenTelemetry.Trace;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PersonService>();
builder.Services.AddHostedService<PersonPostProcessor>();

builder.Services
    .AddGraphQLServer()
    .AddExampleTypes()
    .AddInstrumentation()
    .AddMutationConventions()
    .AddGlobalObjectIdentification(false)
    .AddBananaCakePopServices(x =>
    {
        x.ApiId = ""; // <-- Replace with your API ID
        x.ApiKey = ""; // <-- Replace with your API key
        x.Stage = "dev";
    });

builder.Services
    .AddLogging(x => x
        .AddBananaCakePopExporter()
        .AddOpenTelemetry(x =>
        {
            x.IncludeFormattedMessage = true;
            x.IncludeScopes = true;
        }));

builder.Services
       .AddOpenTelemetry()
       .WithTracing(x =>
       {
           x.AddSource(App.Trace.Name);
           x.AddAspNetCoreInstrumentation();
           x.AddBananaCakePopExporter();
       });

var app = builder.Build();

app.MapGraphQL();

app.Run();
