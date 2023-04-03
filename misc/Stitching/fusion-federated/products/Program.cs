var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<ProductRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .RegisterService<ProductRepository>();

var app = builder.Build();
app.MapGraphQL();
app.RunWithGraphQLCommands(args);
