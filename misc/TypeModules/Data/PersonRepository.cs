namespace Demo;

public class PersonRepository
{
    public List<Person> _persons = new List<Person>
    {
        new(1, "Amanda"),
        new(2, "Jon"),
        new(3, "Chloe"),
        new(4, "Bill")
    };

    public IEnumerable<Person> GetPersons() => _persons;
}