namespace Demo;

public class PersonNodeResolver
{
    public Task<Person> ResolveAsync(
        [Service] IMongoCollection<Person> collection,
        Guid id)
    {
        return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
