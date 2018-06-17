using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CASportStore.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRepository<Product> _repository;

        public AdminController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.List());

        public ViewResult Edit(int productId) =>
            View(_repository.List().FirstOrDefault(x => x.Id == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(product);
                TempData["message"] = $"{product.Name} has been saved.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // there is something wrong with the data
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
          
            Product deletedProduct = _repository.GetById(productId);

            if (deletedProduct != null)
            {
                _repository.Delete(deletedProduct);

                TempData["message"] = $"{deletedProduct.Name} was deleted.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);

            return RedirectToAction(nameof(Index));
        }
    }
}
