using NLayer.Core.Entities;

namespace NLayer.Core.DTOs;
public class CategoryWithProductsDto : CategoryDto
{
    public IList<ProductDto> Products { get; set; }
}