namespace Demo.Reviews.Types;

[QueryType]
public static class Query
{
    public static async Task<Review?> GetReviewById(
        int id,
        ReviewByIdDataLoader reviewById,
        CancellationToken cancellationToken)
        => await reviewById.LoadAsync(id, cancellationToken);

    public static IQueryable<Review> GetReviews(ReviewContext context)
        => context.Reviews.OrderByDescending(t => t.Id);

    public static async Task<Author?> GetAuthorById(
        int id,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken)
        => await userById.LoadAsync(id, cancellationToken);

    public static async Task<IReadOnlyList<Author>?> GetAuthorsById(
        int[] ids,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken)
        => await userById.LoadAsync(ids, cancellationToken);

    public static IQueryable<Author>? GetAuthors(ReviewContext context)
        => context.Authors.OrderBy(t => t.Name);
}