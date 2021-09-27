using Demo.Data;
using Demo.Resolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<PersonRepository>()
    .AddGraphQLServer()
    .AddDocumentFromFile("./Schema.graphql")
    .AddResolver<Query>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
