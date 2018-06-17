using CASportStore.Core.Services;
using CASportStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CASportStore.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private CartService _cartService;

        public CartSummaryViewComponent(CartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService);
        }
    }
}
