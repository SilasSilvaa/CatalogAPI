using System;
using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Catalog>> Get()
        {
            var catalogs = _context.Catalogs.ToList();
            
            if (catalogs is null) {
                return NotFound("Categorias não encontradas...");
            }

            return catalogs;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Catalog>> GetProductsCatalogs()
        {
            var products = _context.Catalogs.Include(p => p.Products).ToList();
            
            if(products is null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpGet("{id:int}", Name = "GetCatalgo")]
        public ActionResult<Catalog> Get(int id)
        {
            var catalog = _context.Catalogs.FirstOrDefault(p => p.Equals(id));

            if (catalog == null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(catalog);
        }

        [HttpPost]
        public ActionResult Post(Catalog catalog)
        {
            if (catalog is null)
            {
                return BadRequest();
            }

            _context.Catalogs.Add(catalog);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCatalog", new { id = catalog.CatalogId }, catalog);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,  Catalog catalog) 
        {
            if(id != catalog.CatalogId)
            {
                return BadRequest();
            }

            _context.Entry(catalog).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(catalog);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var catalog = _context.Catalogs.FirstOrDefault(p => p.CatalogId.Equals(id));

            if(catalog == null)
            {
                return NotFound();
            }

            _context.Catalogs.Remove(catalog);
            _context.SaveChanges();

            return Ok(catalog);
        }

    }
}
