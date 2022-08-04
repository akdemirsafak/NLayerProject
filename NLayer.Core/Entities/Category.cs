namespace NLayer.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public IList<Product> Products { get; set; } //Navigation prop. IList
}