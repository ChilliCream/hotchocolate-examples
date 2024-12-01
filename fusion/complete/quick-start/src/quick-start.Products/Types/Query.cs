using HotChocolate.Fusion.SourceSchema.Types;

namespace quick_start.Products.Types;

[QueryType]
public static class Query
{
    public static Product[] GetProducts()
    {
        return
        [
            new Product
            {
                Id = 1,
                Name = "Product 1",
                Sku = "SKU1",
                Description = "Description 1",
                Price = 1.0m
            },
            new Product
            {
                Id = 2,
                Name = "Product 2",
                Sku = "SKU2",
                Description = "Description 2",
                Price = 2.0m
            },
            new Product
            {
                Id = 3,
                Name = "Product 3",
                Sku = "SKU3",
                Description = "Description 3",
                Price = 3.0m
            },
            new Product
            {
                Id = 4,
                Name = "Product 4",
                Sku = "SKU4",
                Description = "Description 4",
                Price = 4.0m
            }
        ];
    }
}

public static class ProductOperations
{
    [Query]
    [Lookup]
    [Internal]
    public static Product GetProductById(int id)
    {
        return new Product
        {
            Id = id,
            Name = $"Product {id}",
            Sku = $"SKU{id}",
            Description = $"Description {id}",
            Price = id
        };
    }
}