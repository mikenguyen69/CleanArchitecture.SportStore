using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Core.Services;
using CASportStore.Web.Controllers;
using CASportStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CASportStore.Tests.Integration.Web.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange 
            Mock<IRepository<Order>> mock = new Mock<IRepository<Order>>();

            CartService cart = new CartService();
            Order order = new Order();

            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = target.Checkout(order) as ViewResult;

            // Assert - Check if order hasn't been stored 
            mock.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
            // Check if the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Passing invalid to ModelState
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {

            // Arrange - create a mock order repository
            Mock<IRepository<Order>> mock = new Mock<IRepository<Order>>();
            // Arrange - create a cart with one item
            CartService cart = new CartService();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert - check that the order hasn't been passed stored
            mock.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order repository
            Mock<IRepository<Order>> mock = new Mock<IRepository<Order>>();
            // Arrange - create a cart with one item
            CartService cart = new CartService();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act - try to checkout
            RedirectToActionResult result =
                 target.Checkout(new Order()) as RedirectToActionResult;

            // Assert - check that the order has been stored
            mock.Verify(m => m.Update(It.IsAny<Order>()), Times.Once);
            // Assert - check that the method is redirecting to the Completed action
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
