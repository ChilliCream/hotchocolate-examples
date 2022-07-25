namespace RequestPipeline;

public class Query
{
    public Book GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };
}

public class Book
{
    [UseToUpper]
    public string Title { get; set; }

    public Author Author { get; set; }

    public async Task<bool> Wait(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        return true;
    }
}

public class Author
{
    public string Name { get; set; }
}
