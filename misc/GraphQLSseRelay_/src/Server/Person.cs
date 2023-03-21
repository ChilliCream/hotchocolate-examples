using System.Collections.Concurrent;

namespace Server
{
    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static ConcurrentBag<Person> Persons { get; } = new();
    }
}