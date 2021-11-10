namespace Demo;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<Person> GetPersons(
        [Service] IMongoCollection<Person> collection)
        => collection.AsExecutable();

    [UseFirstOrDefault]
    public IExecutable<Person> GetPersonById(
        [Service] IMongoCollection<Person> collection,
        [ID] Guid id)
        => collection.Find(x => x.Id == id).AsExecutable();
}
