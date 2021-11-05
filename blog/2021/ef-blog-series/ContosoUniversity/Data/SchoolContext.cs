
namespace ContosoUniversity.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=uni.db");
    }

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