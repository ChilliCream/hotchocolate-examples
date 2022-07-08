var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UploadType>()
    .AddTypeExtension<AuthorExtensions>()
    .AddMutationConventions();

var app = builder.Build();

app.UseStaticFiles();
app.MapGraphQL();

app.Run();
