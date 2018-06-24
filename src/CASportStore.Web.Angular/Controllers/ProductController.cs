using System.Collections.Generic;
using CASportStore.Core.Entities;
using CASportStore.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CASportStore.Web.Angular2.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context) 
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products);
        }

        [HttpPost]
        public IActionResult SaveProduct([FromBody] Product product) {
            var result = _context.Products.Add(product);
            _context.SaveChanges();
            return Accepted(result);
        } 
    }
}