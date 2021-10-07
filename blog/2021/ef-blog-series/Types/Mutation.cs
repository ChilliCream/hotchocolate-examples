namespace ContosoUniversity;

public class Mutation
{
    /// <summary>
    /// Enroll new student into a course.
    /// </summary>
    public async Task<EnrollStudentPayload> EnrollStudentAsync(
        EnrollStudentInput input,
        SchoolContext context,
        CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = input.FirstMidName,
            LastName = input.LastName,
            EnrollmentDate = DateTime.UtcNow,
            Enrollments = { new() { CourseId = input.CourseId } }
        };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        return new EnrollStudentPayload(student.Id, input.CourseId);
    }
}
