namespace quick_start.Ordering.Types;

public class Order
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<LineItem> Items { get; set; }
}