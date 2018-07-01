using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Api.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var products = await service.GetAsync();

            return Json(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await service.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Json(product);
        }
    }
}
