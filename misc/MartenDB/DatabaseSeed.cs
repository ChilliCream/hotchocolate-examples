using Marten;
using Marten.Schema;

namespace MartenDB;

public class DatabaseSeed: IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.OpenSession();
        var markerId = Guid.Parse("5be76eb2-5e85-41d0-b770-ea1d83f9bbb0");
        var seeded = session.Load<Book>(markerId);
         if (seeded is null)
         {
             var randomNumberGenerator =new Random();
             var adjectives = new List<string>
             {
                 "fantastic",
                 "spectacular",
                 "cool"
             };
             var markerDocument = new Book
             {
                 Id = markerId,
                 Content = Guid.NewGuid().ToString(),
                 Rating = new Rating {Score = 0},
                 IsPopular = false
             };
             session.Store(markerDocument);
             for (var i = 0; i < 20; i++)
             {
                 var adjectiveIndex = randomNumberGenerator.Next(0, adjectives.Count);
                 var adjective = adjectives[adjectiveIndex];
                 var book = new Book
                 {
                     Id = Guid.NewGuid(),
                     Content = $"I am a {adjective} book.",
                     Rating = new Rating {Score = randomNumberGenerator.Next(0, 101)},
                     IsPopular = i % 2 == 0
                 };
                 session.Store(book);
             }
         }
         await session.SaveChangesAsync(cancellation);
    }
}