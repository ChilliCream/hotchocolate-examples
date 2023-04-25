using System.Runtime.CompilerServices;
using HotChocolate.Authorization;

[SubscriptionType]
public static class Subscription
{
    [Authorize(Policy = "Admin")]
    [Subscribe(With = nameof(CreateOnPublishedStream))]
    public static Book OnPublished([EventMessage] Book book) => book;

    public static async IAsyncEnumerable<Book> CreateOnPublishedStream(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await Task.Delay(150, cancellationToken);
        yield return new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet", 
                Address = "London"
            }
        };

        await Task.Delay(500, cancellationToken);
        yield return new Book
        {
            Title = "C# in depth. - 2nd Edition",
            Author = new Author
            {
                Name = "Jon Skeet", 
                Address = "London"
            }
        };

        await Task.Delay(250, cancellationToken);
        yield return new Book
        {
            Title = "C# in depth. - 3rd Edition",
            Author = new Author
            {
                Name = "Jon Skeet", 
                Address = "London"
            }
        };
        
        await Task.Delay(300, cancellationToken);
        yield return new Book
        {
            Title = "C# in depth. - 4th Edition",
            Author = new Author
            {
                Name = "Jon Skeet", 
                Address = "London"
            }
        };
    }
}