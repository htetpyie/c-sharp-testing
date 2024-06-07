using APIApp.Models;

namespace APIApp.GraphQl
{
    public class Query
    {
        private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Smartphone", Price = 799.99m },
    };

        public List<Product> GetProducts() => _products;
    }

    public class Mutation
    {
        public Product AddProduct(ProductInput input)
        {
            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price
            };
            return product;
        }
    }

    public class ProductInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Price).Type<NonNullType<DecimalType>>();
        }
    }

    public class ProductInputType : InputObjectType<ProductInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ProductInput> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Price).Type<NonNullType<DecimalType>>();
        }
    }
}