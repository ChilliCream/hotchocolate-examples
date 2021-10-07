namespace ContosoUniversity;

public class Query
{
    /// <summary>
    /// Gets all students.
    /// </summary>
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Student> GetStudents(SchoolContext schoolContext)
        => schoolContext.Students;

    /// <summary>
    /// Gets all courses.
    /// </summary>
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Course> GetCourses(SchoolContext context)
        => context.Courses;
}
