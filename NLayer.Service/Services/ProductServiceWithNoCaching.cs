using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class ProductServiceWithNoCaching : Service<Product>, IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productSeRepository;

    public ProductServiceWithNoCaching(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork,
        IProductRepository productSeRepository, IMapper mapper) :
        base(genericRepository,
            unitOfWork) //Bu parametreler private olduğu için constructorda olmasına rağmen ulaşılamazlar.
    {
        _productSeRepository = productSeRepository;
        _mapper = mapper;
    }


    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var products = await _productSeRepository.GetProductsWithCategory();
        var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
    }
}