using CASportStore.Core.Entities;
using CASportStore.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CASportStore.Web.Angular.Controllers
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
            var result = _context.Products;
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SaveProducts([FromBody] Product product)
        {
            _context.Products.AddAsync(product);
            var result = _context.SaveChanges();

            return Accepted(result);
        }
    }
}