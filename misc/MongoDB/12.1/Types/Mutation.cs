namespace Demo;

public class Mutation
{
    public async Task<CreatePersonPayload> CreatePersonAsync(
        [Service] IMongoCollection<Person> collection,
        CreatePersonInput input)
    {
        var person = new Person()
        {
            Name = input.Name,
            Addresses = input.Addresses,
            MainAddress = input.MainAddress
        };

        await collection.InsertOneAsync(person);

        return new CreatePersonPayload(person);
    }
}
