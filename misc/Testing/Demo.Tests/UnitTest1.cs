using System.Text;
using Microsoft.Extensions.DiagnosticAdapter.Internal;

namespace Demo.Tests;

public class SimpleTests
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchBookAndAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { title author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor1()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ author { name } }"));

        result.MatchSnapshot();
    }
}

public class SimpleTests2
{
    [Fact]
    public async Task FetchAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchBookAndAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { title author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor1()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ author { name } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task Subscribe()
    {
        using var cts = new CancellationTokenSource(1000);
        var stream = TestServices.ExecuteRequestAsStreamAsync(
            b => b.SetQuery("subscription { onFoo }"),
            cts.Token);

        var list = new List<string>();

        await foreach (string result in stream)
        {
            list.Add(result);
        }

        list.MatchSnapshot();
    }
}

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddSubscriptionType<Subscription>()
            .Services
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<string> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        await using var scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        await using var result = await Executor.ExecuteAsync(request, cancellationToken);

        result.ExpectQueryResult();

        return result.ToJson();
    }

    public static async IAsyncEnumerable<string> ExecuteRequestAsStreamAsync(
        Action<IQueryRequestBuilder> configureRequest,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        await using var result = await Executor.ExecuteAsync(request, cancellationToken);

        await foreach (var element in result.ExpectResponseStream().ReadResultsAsync().WithCancellation(cancellationToken))
        {
            await using (element)
            {
                yield return element.ToJson();
            }
        }
    }
}