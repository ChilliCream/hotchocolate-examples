namespace RestIntegration;

public class Query
{
    public Book? GetBookById(int id, [Service] BooksRepository repository)
        => repository.GetById(id);

    public IEnumerable<Book> GetBooks([Service] BooksRepository repository)
        => repository.GetAllBooks();
}
