namespace quick_start.Products.Types;



[InterfaceType]
public interface IProduct
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Sku { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}

[ObjectType]
public class Product : IProduct
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Sku { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}

[ObjectType]
public class DigitalProduct : Product
{
    public string LicenseKey { get; set; }
}

[ObjectType]
public class PhysicalProduct : Product
{
    public decimal Height { get; set; }

    public decimal Width { get; set; }
    
    public decimal Weight { get; set; }
}