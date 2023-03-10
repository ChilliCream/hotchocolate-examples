using Demo.Gateway.Helpers;
using HotChocolate.Fusion.Clients;
using HotChocolate.Fusion.Composition;
using HotChocolate.Skimmed.Serialization;

var builder = WebApplication.CreateBuilder(args);

var project = await DemoProject.CreateAsync();
var fusionGraph = await new FusionGraphComposer().ComposeAsync(
    new[] 
    {
        project.Accounts.ToConfiguration(),
        project.Reviews.ToConfiguration(),
        project.Products.ToConfiguration(),
    });

builder.Services.AddHttpClient("DefaultClient");
builder.Services.AddSingleton<IWebSocketConnectionFactory, DemoWebSocketConnectionFactory>();

builder.Services
    .AddFusionGatewayServer(
        SchemaFormatter.FormatAsString(fusionGraph));

var app = builder.Build();
app.MapGraphQL();
app.RunWithGraphQLCommands(args);
