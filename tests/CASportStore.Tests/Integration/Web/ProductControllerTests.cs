using CASportStore.Web.Models;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CASportStore.Web.Controllers;
using CASportStore.Web.Models.ViewModels;

namespace CASportStore.Tests.Integration.Web
{
    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] array = result.Products.ToArray();
            Assert.True(array.Length == 2);
            Assert.Equal("P4", array[0].Name);
            Assert.Equal("P5", array[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            // Act 
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Product()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1", Category="Cat1"},
                new Product {Id = 2, Name = "P2", Category="Cat1"},
                new Product {Id = 3, Name = "P3", Category="Cat1"},
                new Product {Id = 4, Name = "P4", Category="Cat2"},
                new Product {Id = 5, Name = "P5", Category="Cat2"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act
            ProductsListViewModel result = controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] array = result.Products.ToArray();
            Assert.True(array.Length == 2);
            Assert.True("P4" == array[0].Name && array[0].Category == "Cat2");
            Assert.True("P5" == array[1].Name && array[0].Category == "Cat2");
        }
    }
}
