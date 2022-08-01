using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class ProductController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IProductService _service;


    public ProductController(IMapper mapper, IProductService service)
    {
        _mapper = mapper;
        _service = service;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
        //return Ok(CustomResponseDto<List<ProductDto>>.Success(200,productsDto));
        return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
    }
[ServiceFilter(typeof(NotFoundFilter<Product>))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var product = await _service.GetByIdAsync(id);
        var productDto = _mapper.Map<ProductDto>(product);

        return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ProductDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var resultDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, resultDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductUpdateDto productDto)
    {
        await _service.UpdateAsync(_mapper.Map<Product>(productDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetByIdAsync(id);
        await _service.RemoveAsync(product);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    //api/products/GetProductsWithCategory
    [HttpGet("GetProductsWithCategory")]
    public async Task<IActionResult> GetProductsWithCategory()
    {
        return CreateActionResult(await _service.GetProductsWithCategory());
    }
}