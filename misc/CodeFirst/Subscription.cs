using HotChocolate;
using HotChocolate.Types;
using StarWars.Models;

namespace StarWars
{
    public class Subscription
    {
        [Subscribe]
        public Review OnReview(
            [Topic]Episode episode, 
            [EventMessage]Review message) =>
            message;
    }
}
