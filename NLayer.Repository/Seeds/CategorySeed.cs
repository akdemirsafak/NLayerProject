using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.Seeds;

public class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category
            {
                Id = 1,
                Name = "Kalem",
                CreatedDate = DateTime.Now
            }, new Category
            {
                Id = 2,
                Name = "Kitap",
                CreatedDate = DateTime.Now
            }, new Category
            {
                Id = 3,
                Name = "Defter",
                CreatedDate = DateTime.Now
            }
            , new Category
            {
                Id = 4,
                Name = "Silgi",
                CreatedDate = DateTime.Now
            });
    }
}