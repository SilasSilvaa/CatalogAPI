using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models;

[Table("Catalogs")]
public class Catalog
{
    public Catalog()
    {
        Products = new List<Product>();
    }

    [Key]
    public int CatalogId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Image { get; set; }
    public ICollection<Product> Products { get; set; }
}
