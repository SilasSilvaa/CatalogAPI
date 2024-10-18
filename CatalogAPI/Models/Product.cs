using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(300)]
    public string? Image { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Inventory { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int CatalogId { get; set; }
    public Catalog? Catalog { get; set; }
}
