using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.Configurations;

public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.HasOne(x => x.Product)
            .WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);
    }
}