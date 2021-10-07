using HotChocolate.Execution.Configuration;

namespace ContosoUniversity;

public static class DatabaseInitializer
{
    public static IRequestExecutorBuilder EnsureDataIsSeeded(this IRequestExecutorBuilder builder)
        => builder.ConfigureSchemaAsync(async (s, b, ct) =>
        {
            using var scope = s.GetApplicationServices().CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            await SeedDataAsync(context);
        });

    private static async Task SeedDataAsync(SchoolContext context)
    {
        if (context.Database.EnsureCreated())
        {
            var course = new Course { Credits = 10, Title = "Object Oriented Programming 1" };

            context.Enrollments.Add(new Enrollment
            {
                Course = course,
                Student = new Student { FirstMidName = "Rafael", LastName = "Foo", EnrollmentDate = DateTime.UtcNow }
            });

            context.Enrollments.Add(new Enrollment
            {
                Course = course,
                Student = new Student { FirstMidName = "Pascal", LastName = "Bar", EnrollmentDate = DateTime.UtcNow }
            });

            context.Enrollments.Add(new Enrollment
            {
                Course = course,
                Student = new Student { FirstMidName = "Michael", LastName = "Baz", EnrollmentDate = DateTime.UtcNow }
            });

            await context.SaveChangesAsync();
        }
    }
}
