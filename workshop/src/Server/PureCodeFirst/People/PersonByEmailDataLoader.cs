using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.DataLoader;

namespace Chat.Server.People
{
    public class PersonByEmailDataLoader
        : BatchDataLoader<string, Person>
    {
        private readonly IPersonRepository _repository;

        public PersonByEmailDataLoader(IPersonRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<IReadOnlyDictionary<string, Person>> LoadBatchAsync(
            IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return await _repository.GetPeopleByEmailAsync(
                keys, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}