namespace quick_start.Ordering.Types;

[QueryType]
public static class Query
{
    public static Order[] GetOrders() => new[]
    {
        new Order
        {
            Id = 1,
            Name = "Order 1",
            Description = "Description 1",
            Items =
            [
                new() { Id = 1, Quantity = 1, ProductId = 1 },
                new() { Id = 2, Quantity = 2, ProductId = 2 }
            ]
        },
        new Order
        {
            Id = 3,
            Name = "Order 3",
            Description = "Description 3",
            Items =
            [
                new() { Id = 3, Quantity = 3, ProductId = 3 },
                new() { Id = 4, Quantity = 4, ProductId = 4 }
            ]
        }
    };
}