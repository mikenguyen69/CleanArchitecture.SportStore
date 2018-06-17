using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Core.Services;
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
        private IRepository<Order> _repository;
        private CartService _cartService;

        public OrderController(IRepository<Order> repository, CartService cartService)
        {
            _repository = repository;
            _cartService = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cartService.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cartService.Lines.ToArray();
                _repository.Update(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View();
            }
        }

        public ViewResult Completed()
        {
            _cartService.Clear();
            return View();
        }

        [Authorize]
        public ViewResult List() =>
            View(_repository.List().Where(o => !o.Shipped));

        [Authorize]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _repository.List().FirstOrDefault(x => x.Id == orderId);

            if (order != null)
            {
                order.Shipped = true;
                _repository.Update(order);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
