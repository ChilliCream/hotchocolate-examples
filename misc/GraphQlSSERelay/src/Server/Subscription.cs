using HotChocolate;
using HotChocolate.Types;

namespace Server
{
    public class Subscription
    {
        [Subscribe]
        public Person PersonAdded([EventMessage] Person person)
        {
            return person;
        }
    }
}