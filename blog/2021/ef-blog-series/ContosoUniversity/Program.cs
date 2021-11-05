var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType()
        .AddTypeExtension<CreateCourseMutation>()
        .AddTypeExtension<EnrollStudentMutation>()
        .AddTypeExtension<GradeStudentMutation>()
        .AddTypeExtension<RegisterStudentMutation>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
