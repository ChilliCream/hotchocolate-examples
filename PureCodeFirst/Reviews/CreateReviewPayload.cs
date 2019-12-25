using StarWars.Characters;
using StarWars.Models;

namespace StarWars.Reviews
{
    public class CreateReviewPayload
    {
        public CreateReviewPayload(Episode episode, Review review)
        {
            Episode = episode;
            Review = review;
        }

        public Episode Episode { get; }

        public Review Review { get; }
    }
}
