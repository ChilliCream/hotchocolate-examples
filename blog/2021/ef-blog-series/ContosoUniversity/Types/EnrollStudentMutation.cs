namespace ContosoUniversity.Types;

public record EnrollStudentPayload(int EnrollmentId)
{
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Enrollment> GetEnrollment(SchoolContext context)
        => context.Enrollments.Where(e => e.EnrollmentId == EnrollmentId);
}

public record EnrollStudentInput(int StudentId, int CourseId);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class EnrollStudentMutation
{
    public async Task<EnrollStudentPayload> EnrollStudentAsync(
        EnrollStudentInput input,
        SchoolContext schoolContext)
    {
        var enrolment = new Enrollment
        {
            CourseId = input.CourseId,
            StudentId = input.StudentId,
        };

        schoolContext.Enrollments.Add(enrolment);
        await schoolContext.SaveChangesAsync();

        return new EnrollStudentPayload(enrolment.EnrollmentId);
    }
}