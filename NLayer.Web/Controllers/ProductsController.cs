using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;


    public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetProductsWithCategory());
    }

    [HttpGet]
    public async Task<IActionResult> Save()
    {
        var categories = await _categoryService.GetAllAsync();
        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList()); //.ToList();
        ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name"); //2. parametre seçilen değer 3. parametre kullanıcıya gösterilecek değer.
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Save(ProductDto productDto)
    {

        if (ModelState.IsValid)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return RedirectToAction(nameof(Index));
        }

        var categories = await _categoryService.GetAllAsync();
        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList()); //ToList():
        ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name"); //2. parametre seçilen değer 3. parametre kullanıcıya gösterilecek değer.
        return View();

    }
}