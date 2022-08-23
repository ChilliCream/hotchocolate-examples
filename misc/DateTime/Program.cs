var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPooledDbContextFactory<NotesContext>(o => o.UseSqlite("Data Source=notes.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddMutationConventions()
    .RegisterDbContext<NotesContext>(DbContextKind.Pooled)
    .AddTypeConverter<DateTimeOffset, DateTime>(t => t.UtcDateTime)
    .AddTypeConverter<DateTime, DateTimeOffset>(
        t => t.Kind is DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(t, DateTimeKind.Utc)
            : t);

var app = builder.Build();

app.MapGraphQL();

app.Run();
