# Fetching data with GraphQL

## Part 1

1. Run `dotnet watch --no-hot-reload` so that we can test each part and there is no need to kill and restart the server manually all the time.

1. Add a new file `Types/Query.cs` and pase the following code.

```csharp
namespace Demo.Types;

public class Query
{
    public IQueryable<Asset> GetAssets(AssetContext context)
        => context.Assets;
}
```

2. Head over to the `Program.cs` and replace `builder.Services.AddGraphQLServer();` with the following piece of code:

```csharp
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterDbContext<AssetContext>(DbContextKind.Pooled);
```

## Part 2

1. Head over to the `Query.cs` and add the `UsePaging` attribute to your `Resolver`.

```csharp
namespace Demo.Types;

public class Query
{
    [UsePaging]
    public IQueryable<Asset> GetAssets(AssetContext context)
        => context.Assets;
}
```

## Part 3

1. Head over to the `Program.cs` and replace the GraphQL configuration chain with the following code:

```csharp
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<AssetContext>(DbContextKind.Pooled);
```

2. Head over to the `Query.cs` and add the Hot Chocolate data annotations.

```csharp
namespace Demo.Types;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Asset> GetAssets(AssetContext context)
        => context.Assets;
}
```