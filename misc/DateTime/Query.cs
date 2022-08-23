namespace Demo;

public class Query
{
    [UsePaging]
    public IQueryable<Note> GetNotes(NotesContext context)
        => context.Notes;

    public async Task<Note?> GetNoteById(int id, NotesContext context)
        => await context.Notes.FindAsync(id);
}
