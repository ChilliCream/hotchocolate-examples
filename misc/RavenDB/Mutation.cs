using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Types;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace raven;

public class Mutation
{
    public async Task<Person> CreatePersonAsync(
        IAsyncDocumentSession session, string name, CancellationToken cancellationToken)
    {
        var person = new Person() { Name = name };
        
        await session.StoreAsync(person, cancellationToken);

        await session.SaveChangesAsync(cancellationToken);
        return person;
    }

    [Error<PersonNotFoundException>]
    public async Task<Person> UpdateNameOfPersonAsync(
        IAsyncDocumentSession session,
        string id,
        string name, CancellationToken cancellationToken)
    {
        var person = await session.Query<Person>()
            .FirstOrDefaultAsync(x => x.Id == id, token: cancellationToken);

        if (person is null)
        {
            throw new PersonNotFoundException(id);
        }

        person.Name = name;

        await session.SaveChangesAsync(cancellationToken);

        return person;
    }
}
