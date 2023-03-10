using Demo.Reviews.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<ReviewRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .RegisterService<ReviewRepository>();

var app = builder.Build();
app.UseWebSockets();
app.MapGraphQL();
app.Run();
