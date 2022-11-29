using System.Collections.Immutable;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<AuthorResolvers>();
    /*
    // global middleware to build up a path object
    .UseField(next => async context =>
    {
        await next(context);

        var path = context.GetScopedStateOrDefault<ImmutableStack<object>>("path") ??
            ImmutableStack<object>.Empty;

        if (context.Result is not null)
        {
            context.SetScopedState("path", path.Push(context.Result));
        }
    });
    */

var app = builder.Build();

app.MapGraphQL();

app.Run();
