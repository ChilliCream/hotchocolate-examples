namespace Demo.Products.Types;

[QueryType]
public class Query
{
    public IEnumerable<Product> GetTopProducts(
        int first,
        [Service] ProductRepository repository) =>
        repository.GetTopProducts(first);

    [NodeResolver]
    public Product GetProductById(
        int id,
        [Service] ProductRepository repository) =>
        repository.GetProduct(id);
    
    public IEnumerable<Product> GetProductsById(
        [ID<Product>] int[] ids,
        [Service] ProductRepository repository)
    {
        foreach (var id in ids)
        {
            yield return repository.GetProduct(id);
        }
    }
}
