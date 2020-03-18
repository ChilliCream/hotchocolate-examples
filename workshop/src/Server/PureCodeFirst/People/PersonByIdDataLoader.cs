using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.DataLoader;

namespace Chat.Server.People
{
    public class PersonByIdDataLoader
        : BatchDataLoader<Guid, Person>
    {
        private readonly IPersonRepository _repository;

        public PersonByIdDataLoader(IPersonRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Person>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _repository.GetPeopleAsync(
                keys, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}