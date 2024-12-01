var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddGraphQL().AddTypes();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
