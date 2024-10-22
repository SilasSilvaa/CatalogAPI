using System;
using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return _context.Products.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    "Ocorreu um erro ao processar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                return _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId.Equals(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    "Ocorreu um erro ao processar sua solicitação");
            }

        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetProduct",
                    new { id = product.ProductId }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Ocorreu um erro ao processar sua solicitação");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(product);
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
                var product = _context.Products.FirstOrDefault(p => p.ProductId.Equals(id));

                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    "Ocorreu um erro ao processar sua solicitação");
            }
        }
    }
}
