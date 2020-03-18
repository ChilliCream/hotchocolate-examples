using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate.DataLoader;

namespace Chat.Server.People
{
    public class PersonByEmailDataLoader
        : BatchDataLoader<string, Person>
    {
        private readonly ChatDbContext _dbContext;

        public PersonByEmailDataLoader(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IReadOnlyDictionary<string, Person>> LoadBatchAsync(
            IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return await _dbContext.People
                .Where(t => keys.Contains(t.Email))
                .ToDictionaryAsync(t => t.Email);
        }
    }
}