namespace Demo.Reviews.Types;

[QueryType]
public static class Query
{
    public static IEnumerable<Review> GetReviews(
        ReviewRepository repository) =>
        repository.GetReviews();

    [NodeResolver]
    public static Review? GetReviewById(
        ReviewRepository repository,
        int id) =>
        repository.GetReview(id);
    
    public static IEnumerable<Review> GetReviewsById(
        ReviewRepository repository,
        [ID<Review>] int[] ids)
    {
        foreach (var id in ids)
        {
            var user = repository.GetReview(id);

            if (user is not null)
            {
                yield return user;
            }
        }
    }

    public static User GetUserById(
        ReviewRepository repository,
        [ID<User>] int id)
        => new User(id, "some name");

    public static Product GetProductById(
        ReviewRepository repository,
        [ID<Product>] int id)
        => new Product(id);
}
