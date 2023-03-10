namespace Demo.Reviews.Types;

[QueryType]
public static class Query
{
    public static IEnumerable<Review> GetReviews(
        ReviewRepository repository) =>
        repository.GetReviews();

    public static Author GetAuthorById(
        ReviewRepository repository,
        int id)
        => new Author(id, "some name");

    public static Product GetProductById(
        ReviewRepository repository,
        int upc)
        => new Product(upc);
}
