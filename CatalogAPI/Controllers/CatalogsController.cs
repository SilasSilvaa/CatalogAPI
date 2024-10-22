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
            try
            {
                return _context.Catalogs.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao processar sua solicitação");
            }
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Catalog>> GetProductsCatalogs()
        {
            try
            {
                return _context.Catalogs.AsNoTracking().Include(p => p.Products).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao processar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "GetCatalgo")]
        public ActionResult<Catalog> Get(int id)
        {
            try
            {
                var catalog = _context.Catalogs.AsNoTracking().FirstOrDefault(p => p.Equals(id));
                return Ok(catalog);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao processar sua solicitação");
            }


        }

        [HttpPost]
        public ActionResult Post(Catalog catalog)
        {
            try
            {
                _context.Catalogs.Add(catalog);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCatalog", new { id = catalog.CatalogId }, catalog);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Ocorreu um erro ao processar sua solicitação");
            }            
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,  Catalog catalog) 
        {
            try
            {
                _context.Entry(catalog).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(catalog);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Ocorreu um erro ao processar sua solicitação");
            }
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var catalog = _context.Catalogs.FirstOrDefault(p => p.CatalogId.Equals(id));

                _context.Catalogs.Remove(catalog);
                _context.SaveChanges();

                return Ok(catalog);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    "Ocorreu um erro ao processar sua solicitação");
            }
        }

    }
}
