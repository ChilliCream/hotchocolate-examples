namespace quick_start.Ordering.Types;

public class LineItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }
}

[ObjectType<LineItem>]
public static partial class LineItemType
{
    static partial void Configure(IObjectTypeDescriptor<LineItem> descriptor)
    {
        descriptor.Ignore(x => x.ProductId);
    }

    public static IProduct GetProduct([Parent] LineItem lineItem) => new Product { Id = lineItem.ProductId };
}

[InterfaceType]
public interface IProduct
{
    public int Id { get; set; }
}

[ObjectType]
public class Product : IProduct
{
    public int Id { get; set; }
}

/*
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
*/