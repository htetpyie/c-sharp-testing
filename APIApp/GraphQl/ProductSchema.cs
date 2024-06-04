using APIApp.Models;

namespace APIApp.GraphQl
{
    public class ProductSchema
    {
    }

    public class Query
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Smartphone", Price = 799.99m },
        };

    }
}
