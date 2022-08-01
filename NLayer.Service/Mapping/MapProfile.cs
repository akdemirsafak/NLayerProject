using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using ProductFeature = NLayer.Core.DTOs.ProductFeatureDto;

namespace NLayer.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        //CreateMap<Product,ProductDto>(); bu şekilde product ı productDto ya çevirir fakat alttaki gibi yaparsak productDto yu product a da çevirebiliriz.
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>(); // product eklerken updateden product a çeviririz fakat herhangi bir yerde product ı update e çevirmemize gerek yok.
        CreateMap<Product, ProductWithCategoryDto>();

        CreateMap<Category, CategoryWithProductsDto>();
    }
}