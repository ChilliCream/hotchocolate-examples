using HotChocolate.Fusion.SourceSchema.Types;

namespace quick_start.Products.Types;

[QueryType]
public static class Query
{
    public static IProduct[] GetProducts()
    {
        return ProductRepository.GetProducts();
    }
}

public static class ProductOperations
{
    [Query]
    [Lookup]
    [Internal]
    public static IProduct GetProductById(int id)
    {
        return ProductRepository.GetProducts().FirstOrDefault(x => x.Id == id);
    }
}

public class ProductRepository
{
    public static IProduct[] GetProducts()
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
            new DigitalProduct()
            {
                Id = 3,
                Name = "Digital Product 1",
                Sku = "SKU3",
                Description = "Description 3",
                Price = 5.0m,
                LicenseKey = "ABCDE-12345-QWERT-54321-EDCBA"
            },
            new PhysicalProduct()
            {
                Id = 4,
                Name = "Physical Product 1",
                Sku = "SKU4",
                Description = "Description 4",
                Price = 6.0m,
                Height = 100,
                Width = 150,
                Weight = 25
            }
        ];
    }
}