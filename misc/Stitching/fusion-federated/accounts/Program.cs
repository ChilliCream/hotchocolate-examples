using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<UserRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .RegisterService<UserRepository>();

var app = builder.Build();
app.MapGraphQL();
app.Run();
