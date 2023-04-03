using Demo.Gateway.Helpers;
using HotChocolate.Types;
using HotChocolate.Execution;
using HotChocolate.Fusion.Clients;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("DefaultClient");

builder.Services
    .AddSingleton<IWebSocketConnectionFactory, WebSocketConnectionFactory>();

builder.Services
    .AddFusionGatewayServer("./gateway.zip", watchFileForUpdates: true);

var app = builder.Build();
app.UseWebSockets();
app.MapGraphQL();

app.Run();
