namespace ContosoUniversity.Types;

public record GradeStudentInput(int StudentId, Grade Grade);

public record GradeStudentPayload(int EnrollmentId)
{
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Enrollment> GetEnrollment(SchoolContext context)
        => context.Enrollments.Where(e => e.EnrollmentId == EnrollmentId);
}

public class GradeStudentMutation
{
    public async Task<GradeStudentPayload> GradeStudentAsync(
        GradeStudentInput input, 
        SchoolContext context)
    {
        var enrollment = await context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentId == input.StudentId);

        if (enrollment == null)
        {
            throw new GraphQLException($"Invalid StudentId: {input.StudentId}");
        }

        enrollment.Grade = input.Grade;

        await context.SaveChangesAsync();

        return new GradeStudentPayload(enrollment.EnrollmentId);
    } 
}