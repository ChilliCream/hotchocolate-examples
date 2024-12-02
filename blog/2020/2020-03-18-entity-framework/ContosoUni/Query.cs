using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace ContosoUniversity
{
    public class Query
    {
        [UseFirstOrDefault]
        public IQueryable<Student> GetStudentById([Service] SchoolContext context, int studentId) =>
            context.Students.Where(t => t.Id == studentId);

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Student> GetStudents([Service] SchoolContext context) => context.Students;

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Course> GetCourses([Service] SchoolContext context) => context.Courses;
    }
}
