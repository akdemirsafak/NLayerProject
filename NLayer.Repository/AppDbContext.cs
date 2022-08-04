using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Entities;

namespace NLayer.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    //ProductFeature bağımsız olarak eklenemeyeceği için bu işlem product üzerinden yürütülmeli ve burada ProductFeature tanımlanmamalı!
    public DbSet<ProductFeature> ProductFeatures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly
            .GetExecutingAssembly()); // Configuration'ı implemente eden tüm assembly'leri alır.
        modelBuilder.Entity<ProductFeature>().HasData(
            new ProductFeature
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 10,
                Width = 20,
                ProductId = 1
            },
            new ProductFeature
            {
                Id = 2,
                Color = "Mavi",
                Height = 25,
                Width = 25,
                ProductId = 4
            });
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is BaseEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdatedDate = DateTime.Now;
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                            break;
                        }
                }
            }
        }
        return base.SaveChanges();
    }



    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is BaseEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdatedDate = DateTime.Now;
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                            break;
                        }
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}