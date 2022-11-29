using Marten;

namespace MartenDB;

public class Query
{
    [UseFiltering]
    [UseSorting]
    public IExecutable<Book> GetBooks([Service] IQuerySession querySession)
    {
        var queryable = querySession.Query<Book>();
        return queryable.AsExecutable();
    }
}