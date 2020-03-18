using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HotChocolate.Examples.Paging
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoCollection<User> userCollection)
        {
            _userCollection = userCollection
                ?? throw new ArgumentNullException(nameof(userCollection));
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userCollection.AsQueryable();
        }

        public async Task<ILookup<string, User>> GetUsersByCountry(
            IReadOnlyList<string> countries,
            CancellationToken cancellationToken)
        {
            var filters = new List<FilterDefinition<User>>();

            foreach (string country in countries)
            {
                filters.Add(Builders<User>.Filter.Eq(u => u.Country, country));
            }

            List<User> users = await _userCollection
                .Find(Builders<User>.Filter.Or(filters))
                .ToListAsync(cancellationToken);

            return users.ToLookup(t => t.Country);
        }

        public Task<User> GetUserAsync(ObjectId userId, CancellationToken cancellationToken)
        {
            return _userCollection.Find(c => c.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            return _userCollection.InsertOneAsync(user, new InsertOneOptions(), cancellationToken);
        }

        public async Task<IReadOnlyDictionary<ObjectId, User>> GetUsersAsync(
            IReadOnlyCollection<ObjectId> userIds,
            CancellationToken cancellationToken)
        {
            var filters = new List<FilterDefinition<User>>();
            foreach (ObjectId userId in userIds)
            {
                filters.Add(Builders<User>.Filter.Eq(u => u.Id, userId));
            }

            List<User> users = await _userCollection
                .Find(Builders<User>.Filter.Or(filters))
                .ToListAsync(cancellationToken);

            return users.ToDictionary(t => t.Id);
        }
    }
}
