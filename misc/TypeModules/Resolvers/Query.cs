using HotChocolate;

namespace Demo;

public class Query
{
    public IEnumerable<Person> GetPersons([Service] PersonRepository repository) 
        => repository.GetPersons();

    [GraphQLType("Cat!")]
    public object GetCat() 
        => new();
}
