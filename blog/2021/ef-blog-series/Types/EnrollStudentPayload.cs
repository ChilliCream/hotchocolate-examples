namespace ContosoUniversity;

public class EnrollStudentPayload
{
    public EnrollStudentPayload(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }

    public int StudentId { get; }

    public int CourseId { get; }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Student> GetStudent(SchoolContext context)
        => context.Students.Where(t => t.Id == StudentId);

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Course> GetCourse(SchoolContext context)
        => context.Courses.Where(t => t.CourseId == CourseId);
}
