using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Server.People
{
    public class PersonRepository
        : IPersonRepository
    {
        private readonly IMongoCollection<Person> _persons;

        public PersonRepository(IMongoCollection<Person> persons)
        {
            _persons = persons;

            _persons.Indexes.CreateOne(
                new CreateIndexModel<Person>(
                    Builders<Person>.IndexKeys.Ascending(x => x.Email),
                    new CreateIndexOptions { Unique = true }));

            _persons.Indexes.CreateOne(
                new CreateIndexModel<Person>(
                    Builders<Person>.IndexKeys.Ascending(x => x.UserId),
                    new CreateIndexOptions { Unique = true }));
        }

        public IQueryable<Person> GetPersons() =>
            _persons.AsQueryable();

        public async Task<IReadOnlyDictionary<Guid, Person>> GetPeopleAsync(
            IReadOnlyList<Guid> ids,
            CancellationToken cancellationToken)
        {
            var list = await _persons.AsQueryable()
                .Where(t => ids.Contains(t.Id))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return list.ToDictionary(t => t.Id);
        }

        public async Task<IReadOnlyDictionary<string, Person>> GetPeopleByEmailAsync(
            IReadOnlyList<string> emails,
            CancellationToken cancellationToken)
        {
            var list = await _persons.AsQueryable()
                .Where(t => emails.Contains(t.Email))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return list.ToDictionary(t => t.Email);
        }

        public async Task AddPersonAsync(
            Person person,
            CancellationToken cancellationToken)
        {
            await _persons.InsertOneAsync(
                person,
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task UpdatePersonAsync(
            Person person,
            CancellationToken cancellationToken)
        {
            await _persons.ReplaceOneAsync(
                Builders<Person>.Filter.Eq(t => t.Id, person.Id),
                person,
                options: default(ReplaceOptions),
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task UpdateLastSeenAsync(
            Guid id,
            DateTime lastSeen,
            CancellationToken cancellationToken)
        {
            await _persons.UpdateOneAsync(
                Builders<Person>.Filter.Eq(t => t.Id, id),
                Builders<Person>.Update.Set(t => t.LastSeen, lastSeen),
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task AddFriendIdAsync(
            Guid id,
            Guid friendId,
            CancellationToken cancellationToken)
        {
            await _persons.UpdateOneAsync(
                Builders<Person>.Filter.Eq(t => t.Id, id),
                Builders<Person>.Update.AddToSet(t => t.FriendIds, friendId),
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }
    }
}