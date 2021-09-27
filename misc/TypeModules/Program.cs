using Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<PersonRepository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeModule(sp => new CustomTypeModule("./DynamicTypes/DynamicTypes.json"));

var app = builder.Build();

app.MapGraphQL();

app.Run();
