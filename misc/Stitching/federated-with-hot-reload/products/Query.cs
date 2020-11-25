using System.Collections.Generic;
using HotChocolate;

namespace Demo.Products
{
    public class Query
    {
        public IEnumerable<Product> GetTopProducts( 
            [Service] ProductRepository repository,
            int first = 5) =>
            repository.GetTopProducts(first);

        public Product GetProduct(
            int upc, 
            [Service] ProductRepository repository) =>
            repository.GetProduct(upc);
    }
}