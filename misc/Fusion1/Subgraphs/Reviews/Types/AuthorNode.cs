namespace Demo.Reviews.Types;

[ExtendObjectType<Author>]
internal static class AuthorNode
{
    public static async Task<IEnumerable<Review>> GetReviewsAsync(
        [Parent] Author user,
        ReviewsByUserIdDataLoader reviewsById,
        CancellationToken cancellationToken)
        => await reviewsById.LoadAsync(user.Id, cancellationToken);

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Author>> GetUserByIdAsync(
        IReadOnlyList<int> ids,
        ReviewContext context,
        CancellationToken cancellationToken)
        => await context.Authors
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
}
