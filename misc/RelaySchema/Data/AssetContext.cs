namespace Demo.Data;

public class AssetContext : DbContext
{
    public AssetContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<AssetPrice>()
            .HasOne(t => t.Asset)
            .WithOne(t => t.Price)
            .HasForeignKey<AssetPrice>(t => t.AssetId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Asset> Assets => Set<Asset>();

    public DbSet<AssetPrice> AssetPrices => Set<AssetPrice>();
}