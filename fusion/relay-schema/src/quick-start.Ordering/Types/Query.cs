namespace quick_start.Ordering.Types;

[QueryType]
public static class Query
{
    private static readonly Order[] _orders =
        [
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
                Id = 2,
                Name = "Order 2",
                Description = "Description 2",
                Items =
                [
                    new() { Id = 3, Quantity = 3, ProductId = 3 },
                    new() { Id = 4, Quantity = 4, ProductId = 4 }
                ]
            }
        ];

    [NodeResolver]
    public static Order? GetOrderById(int id) =>

        _orders.FirstOrDefault(x => x.Id == id);

    public static Order[] GetOrders()
    {
        return _orders;
    }
}