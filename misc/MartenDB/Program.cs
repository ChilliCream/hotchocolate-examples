using Marten;
using MartenDB;

var builder = WebApplication.CreateBuilder(args);

var martenConfigurator = builder.Services.AddMarten(_ =>
{
    var options = new StoreOptions();
    var connectionString = builder.Configuration["ConnectionStrings:Marten"];
    options.Connection(connectionString);
    options.DatabaseSchemaName = "martenChocolate";
    return options;
});
martenConfigurator.InitializeWith<DatabaseSeed>();
builder.Services.AddTransient(sp =>
{
    var documentStore = sp.GetRequiredService<IDocumentStore>();
    var querySession = documentStore.QuerySession();
    return querySession;
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMartenSorting()
    .AddMartenFiltering();

var app = builder.Build();
app.MapGraphQL();
app.Run("http://localhost:3000");