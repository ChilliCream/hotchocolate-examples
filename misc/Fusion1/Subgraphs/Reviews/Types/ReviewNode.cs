namespace Demo.Reviews.Types;

[ExtendObjectType<Review>(IgnoreProperties = new[] { nameof(Review.AuthorId) })]
internal static class ReviewNode
{
    public static async Task<Author> GetAuthorAsync(
        [Parent] Review review,
        UserByIdDataLoader userDataLoader,
        CancellationToken cancellationToken)
        => await userDataLoader.LoadAsync(review.AuthorId, cancellationToken);

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Review>> GetReviewByIdAsync(
        IReadOnlyList<int> ids,
        ReviewContext context,
        CancellationToken cancellationToken)
        => await context.Reviews
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);

    [DataLoader]
    internal static async Task<ILookup<int, Review>> GetReviewsByUserIdAsync(
        IReadOnlyList<int> ids,
        ReviewContext context,
        CancellationToken cancellationToken)
    {
        var reviews = await context.Authors
            .Where(t => ids.Contains(t.Id))
            .SelectMany(t => t.Reviews)
            .ToListAsync(cancellationToken);

        return reviews.ToLookup(t => t.AuthorId);
    }
}
