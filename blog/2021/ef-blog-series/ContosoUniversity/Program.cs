var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType()
        .AddTypeExtension<CreateCourseMutation>()
        .AddTypeExtension<EnrollStudentMutation>()
        .AddTypeExtension<GradeStudentMutation>()
        .AddTypeExtension<RegisterStudentMutation>()
    .ConfigureResolverCompiler(c => c.AddService<SchoolContext>())
    .AddFiltering()
    .AddSorting()
    .AddProjections();

var app = builder.Build();

app.MapGraphQL();

app.Services.GetRequiredService<SchoolContext>().Database.EnsureCreated();

app.Run();
