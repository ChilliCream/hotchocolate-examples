using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Data.Raven;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace raven;

public class Query
{
    public Task<Person> GetPersonById(
        IAsyncDocumentSession session,
        string id,
        CancellationToken cancellationToken)
        => session
            .Query<Person>()
            .Include(x => x.Friends)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    [UseFirstOrDefault]
    [UseFiltering]
    public IExecutable<Person> GetPerson(IAsyncDocumentSession session)
        => session.Query<Person>().AsExecutable();

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IRavenQueryable<Person> GetPersons(IAsyncDocumentSession session)
        => session.Query<Person>();
}

[ExtendObjectType<Person>]
public class PersonExtension
{
    public async Task<List<Person>> GetFriends(
        IAsyncDocumentSession session,
        [Parent] Person person,
        CancellationToken cancellationToken)
        => await session
            .Query<Person>()
            .Where(x => x.Id.In(person.Friends))
            .ToListAsync(cancellationToken);
}
