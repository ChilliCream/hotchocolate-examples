namespace quick_start.Products.Types;

public record Product
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Sku { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }
}