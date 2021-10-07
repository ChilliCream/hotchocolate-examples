namespace ContosoUniversity;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string FirstMidName { get; set; } = default!;

    [Required]
    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public Grade? Grade { get; set; }

    public virtual Course Course { get; set; } = default!;

    public virtual Student Student { get; set; } = default!;
}

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
