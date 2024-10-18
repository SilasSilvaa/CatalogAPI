using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    DbSet<Catalog>? Catalogs { get; set; }
    DbSet<Product>? Products { get; set; }

}
