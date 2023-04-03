namespace Demo.Reviews.Types;

public sealed class User
{
    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }

    public string Name { get; }

    public IEnumerable<Review> GetReviews([Service] ReviewRepository repository)
        => repository.GetReviewsByAuthorId(Id);
}
