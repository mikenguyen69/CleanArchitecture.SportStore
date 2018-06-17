using CASportStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Web.Controllers
{
    public class OrderController :  Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;

        public OrderController(IOrderRepository repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.Save(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View();
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }

        [Authorize]
        public ViewResult List() =>
            View(_repository.Orders.Where(o => !o.Shipped));

        [Authorize]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _repository.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order != null)
            {
                order.Shipped = true;
                _repository.Save(order);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
