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

    public static Product GetProduct([Parent] LineItem lineItem)
        => new(lineItem.ProductId);
}

public sealed record Product([property: ID] int Id);