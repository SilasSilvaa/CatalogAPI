namespace CatalogAPI.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string? Inventory { get; set; }
    public DateTime RegistrationData { get; set; }
    public int CatalogId { get; set; }
    public Catalog Catalog { get; set; }
}
