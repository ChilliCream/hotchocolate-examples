namespace RestIntegration;

public sealed class BooksRepository
{
    private readonly Dictionary<int, Book> _books = new()
    {
        [1] = new Book(1, 1, "C# in Depth"),
        [2] = new Book(2, 1, "C# in Depth, second edition"),
        [3] = new Book(3, 2, "Essential Windows Presentation Foundation (WPF)"),
        [4] = new Book(4, 3, "Pro WPF: Windows Presentation Foundation in .NET 3.0"),
    };

    public Book? GetById(int id)
        => _books.TryGetValue(id, out var book) ? book : null;

    public IEnumerable<Book> GetAllBooks()
        => _books.Values;
}
