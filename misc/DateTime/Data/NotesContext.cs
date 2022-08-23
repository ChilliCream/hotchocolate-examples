namespace Demo.Data;

public class NotesContext : DbContext
{
    public NotesContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();
}