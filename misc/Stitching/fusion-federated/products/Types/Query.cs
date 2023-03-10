namespace Demo.Products.Types;

[QueryType]
public class Query
{
    public IEnumerable<Product> GetTopProducts(
        int first,
        [Service] ProductRepository repository) =>
        repository.GetTopProducts(first);

    public Product GetProductById(
        int upc,
        [Service] ProductRepository repository) =>
        repository.GetProduct(upc);

    public IEnumerable<Product> GetProductsById(
        int[] upc,
        [Service] ProductRepository repository)
    {
        foreach (var id in upc)
        {
            yield return repository.GetProduct(id);
        }
    }
}
