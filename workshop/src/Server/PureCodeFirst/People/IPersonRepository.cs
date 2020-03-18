using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server.People
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetPersons();

        Task<IReadOnlyDictionary<Guid, Person>> GetPeopleAsync(
            IReadOnlyList<Guid> ids,
            CancellationToken cancellationToken);

        Task<IReadOnlyDictionary<string, Person>> GetPeopleByEmailAsync(
            IReadOnlyList<string> emails,
            CancellationToken cancellationToken);

        Task AddPersonAsync(
            Person person,
            CancellationToken cancellationToken);

        Task UpdatePersonAsync(
            Person person,
            CancellationToken cancellationToken);

        Task UpdateLastSeenAsync(
            Guid id,
            DateTime lastSeen,
            CancellationToken cancellationToken);

        Task AddFriendIdAsync(
            Guid id,
            Guid friendId,
            CancellationToken cancellationToken);
    }
}