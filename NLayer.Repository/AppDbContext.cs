using System.Reflection;
using Microsoft.EntityFrameworkCore;
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
                Id = 1, Color = "Kırmızı", Height = 10, Width = 20, ProductId = 1
            },
            new ProductFeature
            {
                Id = 2, Color = "Mavi", Height = 25, Width = 25, ProductId = 4
            });
        base.OnModelCreating(modelBuilder);
    }
}