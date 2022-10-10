using HotChocolate.Subscriptions;

namespace SubscriptionDemo;

public class Mutation
{
    public async Task<Book> PublishBook(
        string title,
        string author,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var book = new Book { Title = title, Author = new Author { Name = author } };
        await eventSender.SendAsync(nameof(PublishBook), book, cancellationToken);
        return book;
    }
}
