using System.Collections.Generic;
using StarWars.Characters;
using StarWars.Models;

namespace StarWars.Repositories
{
    public interface IReviewRepository
    {
        void AddReview(Episode episode, Review review);
        IEnumerable<Review> GetReviews(Episode episode);
    }
}
