using AuditExample.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuditService, ConsoleAuditService>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .UseRequest<AuditMiddleware>()
    .UseDefaultPipeline();

var app = builder.Build();
app.MapGraphQL();
app.Run();
