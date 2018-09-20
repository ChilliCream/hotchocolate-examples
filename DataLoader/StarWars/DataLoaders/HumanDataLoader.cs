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

        public HumanDataLoader(CharacterRepository repository)
            : base(new DataLoaderOptions<string>())
        {
        }

        public List<IReadOnlyList<string>> Loads { get; } =
            new List<IReadOnlyList<string>>();

        protected override Task<IReadOnlyList<Result<Human>>> Fetch(
            IReadOnlyList<string> keys)
        {
            // The fetch method has to return a result for
            List<Result<Human>> list = new List<Result<Human>>();
            foreach (Human human in _repository.GetHumans(keys))
            {

            }
        }
    }
}
