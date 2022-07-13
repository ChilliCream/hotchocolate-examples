var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<BooksRepository>();

builder.Services
    .AddHttpClient("rest", c => c.BaseAddress = new Uri("http://localhost:5227"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<BookExtensions>()
    .AddDocumentFromFile("./Schema.graphql")
    .AddJsonSupport()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.Run();
