using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace Server
{
    public class Mutation
    {
        public async Task<Person> AddPerson([Service] ITopicEventSender sender, Person person)
        {
            Person.Persons.Add(person);

            await sender.SendAsync(nameof(Subscription.PersonAdded), person);
            return person;
        }
    }
}