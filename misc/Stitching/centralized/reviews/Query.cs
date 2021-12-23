using System.Collections.Generic;
using HotChocolate;

namespace Demo.Reviews
{
    public class Query
    {
        public IEnumerable<Review> GetReviews(
            [Service] ReviewRepository repository) =>
            repository.GetReviews();

        public IEnumerable<Review> GetReviewsByAuthor(
            [Service] ReviewRepository repository,
            int authorId) =>
            repository.GetReviewsByAuthorId(authorId);

        public IEnumerable<Review> GetReviewsByProduct(
            [Service] ReviewRepository repository,
            int upc) =>
            repository.GetReviewsByProductId(upc);
    }
}
