using CASportStore.Core.Entities;
using CASportStore.Core.Services;
using CASportStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CASportStore.Tests.Core.Entities
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            CartService target = new CartService();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Line()
        {
            // Arrange
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            CartService target = new CartService();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };
            Product p3 = new Product { Id = 3, Name = "P3" };

            CartService target = new CartService();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            target.AddItem(p3, 2);

            target.RemoveLine(p1);
            
            // Assert
            Assert.Empty(target.Lines.Where(x => x.Product == p1));
            Assert.Equal(2, target.Lines.Count());            
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { Id = 2, Name = "P2", Price = 50M };

            CartService target = new CartService();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange
            Product p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { Id = 2, Name = "P2", Price = 50M };

            CartService target = new CartService();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
