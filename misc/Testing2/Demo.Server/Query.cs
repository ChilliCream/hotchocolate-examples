namespace Demo.Server;

[QueryType]
public static class Query
{
    public static Book GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet", 
                Address = "London"
            }
        };

    public static Author GetAuthor() 
        => new Author { Name = "Jon Skeet", Address = "London" };
}
