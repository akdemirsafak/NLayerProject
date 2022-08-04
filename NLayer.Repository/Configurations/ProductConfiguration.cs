using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Price).IsRequired()
            .HasColumnType("decimal(18,2)"); //virgülden sonra 2 karakter toplamda 18 karakterlik ondalıklı sayı alanı.
        builder.HasOne(x => x.Category).WithMany(x => x.Products);
    }
}