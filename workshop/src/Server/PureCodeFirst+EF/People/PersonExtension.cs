using System;
using System.Linq;
using Chat.Server.Messages;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Chat.Server.People
{
    [ExtendObjectType(Name = "Person")]
    public class PersonExtension
    {
        public bool IsOnline([Parent]Person person) =>
            person.LastSeen < DateTime.UtcNow.AddMinutes(10);

        [UsePaging]
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Message> GetMessages(
            [GlobalState]Guid currentPersonId,
            [Parent]Person recipient,
            [Service]ChatDbContext dbContext)
        {
            return dbContext.Messages.Where(t =>
                (t.RecipientId == currentPersonId && t.SenderId == recipient.Id)
                || (t.SenderId == currentPersonId && t.RecipientId == recipient.Id));
        }

        [UsePaging]
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Person> GetFriends(
            [Parent]Person person,
            [Service]ChatDbContext dbContext) =>
            dbContext.People.Where(t => t.Id == person.Id)
                .SelectMany(t => t.Friends.Select(t => t.Friend));
    }
}
