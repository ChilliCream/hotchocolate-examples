using HotChocolate.Language;
using HotChocolate.Utilities.Introspection;

namespace Demo.Gateway.Helpers;

public sealed class DemoProject
{
    private DemoProject(
        DemoSubgraph accounts,
        DemoSubgraph reviews,
        DemoSubgraph products)
    {
        Accounts = accounts;
        Reviews = reviews;
        Products = products;
    }

    public DemoSubgraph Reviews { get; }
    public DemoSubgraph Products { get; }
    public DemoSubgraph Accounts { get; }

    public static async Task<DemoProject> CreateAsync(CancellationToken ct = default)
    {
        var accounts = new Uri("http://localhost:50901/graphql");
        var accountSchema = await DownloadSchemaAsync(accounts, ct);
        var accountExtensions = File.ReadAllText("../accounts/schema.extensions.graphql");
        var reviews = new Uri("http://localhost:50902/graphql");
        var reviewSchema = await DownloadSchemaAsync(reviews, ct);
        var reviewExtensions = File.ReadAllText("../reviews/schema.extensions.graphql");
        var products = new Uri("http://localhost:50903/graphql");
        var productsSchema = await DownloadSchemaAsync(products, ct);
        var productsExtensions = File.ReadAllText("../products/schema.extensions.graphql");

        return new DemoProject(
            new DemoSubgraph("Accounts", accounts, accountSchema, accountExtensions),
            new DemoSubgraph("Reviews", reviews, reviewSchema, reviewExtensions),
            new DemoSubgraph("Products", products, productsSchema, productsExtensions));
    }

    private static async Task<DocumentNode> DownloadSchemaAsync(
        Uri uri,
        CancellationToken ct = default)
    {
        using var client = new HttpClient();
        var introspection = new IntrospectionClient();
        client.BaseAddress = uri;
        return await introspection.DownloadSchemaAsync(client, ct);
    }
}
