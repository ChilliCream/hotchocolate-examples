var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContextPool<ReviewContext>(
        o => o.UseSqlite("Data Source=review.db"));

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .RegisterDbContext<ReviewContext>();

var app = builder.Build();

await DatabaseHelper.SeedDatabaseAsync(app);

app.UseWebSockets();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
