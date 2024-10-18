namespace CatalogAPI.Models;

public class Catalog
{
    public Catalog()
    {
        Products = new List<Product>();
    }
    public int CatalogId { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public ICollection<Product> Products { get; set; }
}
