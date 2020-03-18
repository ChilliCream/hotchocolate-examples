using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Chat.Server.People
{
    [ExtendObjectType(Name = "Query")]
    public class PersonQueries
    {
        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        [Authorize]
        public Task<Person> GetMeAsync(
            [GlobalState]string currentUserEmail,
            PersonByEmailDataLoader personByEmail,
            CancellationToken cancellationToken) =>
            personByEmail.LoadAsync(currentUserEmail, cancellationToken);

        /// <summary>
        /// Gets access to all the people known to this service.
        /// </summary>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Person> GetPeople(
            [Service]IPersonRepository personRepository) =>
            personRepository.GetPersons();

        [Authorize]
        public Task<Person> GetPersonByEmailAsync(
            string email,
            PersonByEmailDataLoader personByEmail,
            CancellationToken cancellationToken) =>
            personByEmail.LoadAsync(email, cancellationToken);

        [Authorize]
        public Task<Person> GetPersonByIdAsync(
            Guid id,
            PersonByIdDataLoader personById,
            CancellationToken cancellationToken) =>
            personById.LoadAsync(id, cancellationToken);
    }
}
