var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<Cat>()
    .AddType<Dog>()
    .ModifyOptions(o => o.EnableOneOf = true);

var app = builder.Build();

app.MapGraphQL();

app.Run();
