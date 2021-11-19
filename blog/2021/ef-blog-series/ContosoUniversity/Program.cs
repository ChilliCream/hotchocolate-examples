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

await using (var serviceScope = app.Services.CreateAsyncScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<SchoolContext>();
    if (await context.Database.EnsureCreatedAsync())
    {
        context.Courses.Add(new Course
        {
            Title = "Computer Science",
            Enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    Student = new Student
                    {
                        LastName = "Doe",
                        FirstName = "John",
                        EnrollmentDate = DateTime.Now
                    }
                }
            }
        });

        await context.SaveChangesAsync();
    }
}

await app.RunAsync();
