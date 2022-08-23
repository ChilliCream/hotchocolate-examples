namespace Demo;

public class Mutation
{
    public async Task<Note> CreateNote(
        string body,
        DateTime created,
        NotesContext context,
        CancellationToken cancellationToken)
    {
        var note = new Note { Body = body, Created = created };
        context.Notes.Add(note);
        await context.SaveChangesAsync(cancellationToken);
        return note;
    }
}