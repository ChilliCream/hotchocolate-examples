namespace ContosoUniversity;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) 
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = default!;

    public DbSet<Enrollment> Enrollments { get; set; } = default!;

    public DbSet<Course> Courses { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(t => t.Enrollments)
            .WithOne(t => t.Student)
            .HasForeignKey(t => t.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(t => new { t.StudentId, t.CourseId })
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasMany(t => t.Enrollments)
            .WithOne(t => t.Course)
            .HasForeignKey(t => t.CourseId);
    }
}
