using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;

namespace NLayer.Caching;

public class ProductServiceWithCaching : IProductService
{
    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey,
                out _)) //TryGetValue boolean değer ve cachedeki datayı döner, biz datayla ilgilenmediğimiz için out un yanında _ kullandık.
        {
            _memoryCache.Set(CacheProductKey,
                _repository.GetProductsWithCategory()
                    .Result); //if ile cache de data var mı baktık eğer data yoksa burada sorgumuzu attık ve datamızı cache'de tuttuk.
        } //Constructor'da asenkron method kullanamadığımız için result'ını aldık.
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
        if (product is null)
        {
            throw new NotFoundException($"{typeof(Product).Name}({id}) not foud");
        }

        return Task.FromResult(product);
    }

    public async Task<Product> AddAsync(Product entity)
    {
        _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entity;
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new Exception("eeh");
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task UpdateAsync(Product entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveAsync(Product entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entities;
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
    {
        var products = _memoryCache.Get<List<Product>>(CacheProductKey);
        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return Task.FromResult(productsWithCategoryDto);
    }

    public async Task CacheAllProductsAsync() //Ortak kullanılacak kısım olduğu için method'unu yazdık.
    {
        //_memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
    }
}