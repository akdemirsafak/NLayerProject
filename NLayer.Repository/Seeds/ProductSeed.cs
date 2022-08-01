using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.Seeds;

public class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Kurşun Kalem",
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Tükenmez Kalem",
                Price = 150,
                Stock = 50,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "Z kuşağını anlamak",
                Price = 50,
                Stock = 2,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 4,
                CategoryId = 3,
                Name = "Kareli 120 sayfa",
                Price = 5,
                Stock = 500,
                CreatedDate = DateTime.Now
            }
        );
    }
}