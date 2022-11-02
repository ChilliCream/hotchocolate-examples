var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddSubscriptionType<Subscription>();

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class Subscription
{
    [SubscribeAndResolve]
    public async IAsyncEnumerable<string> OnFoo(CancellationToken cancellationToken)
    {
        await Task.Delay(1500, cancellationToken);
        yield return "a";
        await Task.Delay(10, cancellationToken);
        yield return "b";
        await Task.Delay(10, cancellationToken);
        yield return "c";
        await Task.Delay(10, cancellationToken);
        yield return "d";
    }
}