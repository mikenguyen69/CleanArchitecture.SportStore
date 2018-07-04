using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CASportStore.Web.Models.ViewModels;
using CASportStore.Core.Interfaces;
using CASportStore.Core.Entities;
using CASportStore.Core.Services;

namespace CASportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Product> _repository;
        private CartService _cartService;

        public CartController(IRepository<Product> repository, CartService cartService)
        {
            _repository = repository;
            _cartService = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                CartService = _cartService,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product p = _repository.List().FirstOrDefault(x => x.Id == productId);

            if (p != null)
            {
                _cartService.AddItem(p, 1);              
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.List()
                .FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {                
                _cartService.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        
    }
}
