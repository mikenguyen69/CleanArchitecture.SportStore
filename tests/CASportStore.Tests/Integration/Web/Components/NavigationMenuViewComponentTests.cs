using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Web.Components;
using CASportStore.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CASportStore.Tests.Integration.Web.Components
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns((new Product[] {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 2, Name = "P2", Category = "Apples"},
                new Product {Id = 3, Name = "P3", Category = "Plums"},
                new Product {Id = 4, Name = "P4", Category = "Oranges"},
            }).ToList());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);

            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)(target.Invoke()
                as ViewViewComponentResult).ViewData.Model).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples",
                "Oranges", "Plums" }, results));
        }
        
        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            string categoryToSelect = "Apples";
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns((new Product[] {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 4, Name = "P2", Category = "Oranges"},
            }).ToList());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        RouteData = new RouteData()
                    }
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            // Action
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            // Assert 
            Assert.Equal(categoryToSelect, result);
        }
    }
}
