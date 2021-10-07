var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<SchoolContext>(
        (s, o) => o
            .UseSqlite("Data Source=uni.db")
            .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()))
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .ConfigureResolverCompiler(c => c.AddService<SchoolContext>())
    .ModifyOptions(o => o.DefaultResolverStrategy = ExecutionStrategy.Serial)
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .EnsureDataIsSeeded()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.Run();

