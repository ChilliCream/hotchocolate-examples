var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddExtendingTypesTypes()
    .AddDocumentFromFile("./Schema.graphql")
    .AddResolver<AuthorResolvers>("Author");

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class AuthorResolvers
{
    public DateTime GetBirthdate() => DateTime.Now;
}