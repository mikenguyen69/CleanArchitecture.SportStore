using CASportStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CASportStore.Web.Models.ViewModels;

namespace CASportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart _cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product p = _repository.Products.FirstOrDefault(x => x.Id == productId);

            if (p != null)
            {
                _cart.AddItem(p, 1);              
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {                
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        
    }
}
