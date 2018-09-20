using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDonut;
using StarWars.Models;

namespace StarWars.Data
{
    public class HumanDataLoader
        : DataLoaderBase<string, Human>
    {
        private readonly CharacterRepository _repository;

        // You can further configure caching and batching options with the DataLoaderOptions.
        public HumanDataLoader(CharacterRepository repository)
            : base(new DataLoaderOptions<string>())
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<IReadOnlyList<string>> Loads { get; } =
            new List<IReadOnlyList<string>>();

        protected override Task<IReadOnlyList<Result<Human>>> Fetch(
            IReadOnlyList<string> keys)
        {
            // The fetch method has to return a result for each key in the same order as the keys.
            // So if the repository returns less or in a different order this fetch method must return an Array
            // or list of values the same length as the Array of keys, and re-order them to ensure each
            // index aligns with the original keys.
            // https://github.com/facebook/dataloader -> Section Batching

            var result = _repository.GetHumans(keys).ToDictionary(t => t.Id);
            var list = new List<Result<Human>>();

            foreach (string key in keys)
            {
                if (result.TryGetValue(key, out Human human))
                {
                    list.Add(Result<Human>.Resolve(human));
                }
                else
                {
                    // if there was an exception during the resolve use Result<Human>.Reject(error);
                    list.Add(Result<Human>.Resolve(null));
                }
            }

            return Task.FromResult<IReadOnlyList<Result<Human>>>(list);
        }
    }
}
