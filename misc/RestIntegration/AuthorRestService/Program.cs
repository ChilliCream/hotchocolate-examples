using Microsoft.AspNetCore.Mvc;

var data = new Dictionary<int, Author>();
data[1] = new Author(1, "Jon Skeet", "Jon Skeet is a Senior Software Engineer at Google, working in London.", DateTime.Now);
data[2] = new Author(2, "Ian Griffiths", string.Empty, DateTime.Now);
data[3] = new Author(3, "Matthew MacDonald", string.Empty, DateTime.Now);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/authors/{id}", ([FromRoute] int id) =>
{
    if (data.TryGetValue(id, out var author))
    {
        return Results.Ok(author);
    }
    return Results.NotFound();
});

app.Run();

public class Author
{
    public Author(int id, string name, string bio, DateTime birthdate)
    {
        Id = id;
        Name = name;
        Bio = bio;
        Birthdate = birthdate;
    }

    public int Id { get; }

    public string Name { get; }

    public string Bio { get; }

    public DateTime Birthdate { get; }
}