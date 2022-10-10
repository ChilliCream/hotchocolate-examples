using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SubscriptionDemo;

public class Subscription
{
    public async IAsyncEnumerable<Book> OnPublishedStream([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var sourceStream = await eventReceiver.SubscribeAsync<string, Book>(nameof(Mutation.PublishBook), cancellationToken);

        yield return new Book { Title = "First book", Author = new Author { Name = "Some Author" } };

        await Task.Delay(5000, cancellationToken);

        await foreach (Book book in sourceStream.ReadEventsAsync())
        {
            yield return book;
        }
    }

    [Subscribe(With = nameof(OnPublishedStream))]
    public Book OnPublished([EventMessage] Book publishedBook)
        => publishedBook;
}
