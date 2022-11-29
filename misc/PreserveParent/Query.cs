using System.Collections.Immutable;

namespace PreserveParent;

public class Query
{
    [PreserveParentAs("book")]
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
