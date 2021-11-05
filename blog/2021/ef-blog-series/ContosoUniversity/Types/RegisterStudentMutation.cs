namespace ContosoUniversity.Types;

public record RegisterStudentInput(string LastName, string FirstName);

public record RegisterStudentPayload(Student Student);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class RegisterStudentMutation
{
    public async Task<RegisterStudentPayload> RegisterStudentAsync(
        RegisterStudentInput input,
        SchoolContext schoolContext)
    {
        var student = new Student
        {
            LastName = input.LastName,
            FirstName = input.FirstName,
            EnrollmentDate = DateTime.Now
        };

        schoolContext.Students.Add(student);
        await schoolContext.SaveChangesAsync();

        return new RegisterStudentPayload(student);
    }
}