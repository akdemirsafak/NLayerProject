using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsWithCategory()
    {
        // ! Bu örnekte Eager Loading kullandık - Datalar çekilirken kategorilerin de çekilmesini istedik
        return await _context.Products.Include(x => x.Category).ToListAsync();
        // ? Lazy Loading - ihtiyaç olduğu durumda çekmemiz durumudur.
    }
}