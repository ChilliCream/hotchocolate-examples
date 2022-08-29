namespace Demo.Types;

public class Query
{
    [UsePaging]
    [UseProjection]
    public IQueryable<Asset> GetAssets(AssetContext context)
        => context.Assets.OrderBy(t => t.Price!.TradableMarketCapRank);

    [NodeResolver]
    public async Task<Asset?> GetAssetById(
        int id,
        AssetContext context,
        CancellationToken cancellationToken)
        => await context.Assets.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    [NodeResolver]
    public async Task<AssetPrice?> GetAssetPriceById(
        int id,
        AssetContext context,
        CancellationToken cancellationToken)
        => await context.AssetPrices.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
}