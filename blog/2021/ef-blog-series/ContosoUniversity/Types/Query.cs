namespace ContosoUniversity;

public class Query
{
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Student> GetStudentById(int id, SchoolContext context)
        => context.Students.Where(t => t.Id == id);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Student> GetStudents(SchoolContext context)
        => context.Students;

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Course> GetCourseById(int id, SchoolContext context)
        => context.Courses.Where(t => t.CourseId == id);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Course> GetCourses(SchoolContext context)
        => context.Courses;
}
