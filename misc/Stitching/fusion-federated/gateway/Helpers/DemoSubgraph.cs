using HotChocolate.Fusion.Composition;
using HotChocolate.Language;

namespace Demo.Gateway.Helpers;

public sealed class DemoSubgraph
{
    public DemoSubgraph(string name, Uri httpBaseAddress, DocumentNode schema, string extensions)
    {
        Name = name;
        HttpBaseAddress = httpBaseAddress;
        Schema = schema;
        Extensions = extensions;
    }

    public string Name { get; }
    public Uri HttpBaseAddress { get; }
    public DocumentNode Schema { get; }
    public string Extensions { get; }

    public SubgraphConfiguration ToConfiguration()
        => new SubgraphConfiguration(
            Name,
            Schema.ToString(),
            Extensions,
            new IClientConfiguration[] 
            {
                new HttpClientConfiguration(HttpBaseAddress, "DefaultClient"), 
                new WebSocketClientConfiguration(new Uri($"ws://{HttpBaseAddress.Host}:{HttpBaseAddress.Port}/graphql")) 
            });
}
