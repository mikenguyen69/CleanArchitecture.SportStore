using CASportStore.Api;
using CASportStore.Core.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CASportStore.Tests.EndToEnd.Api.Controllers
{
    public class ProductControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        // capture the test inserted product to use later in the delete
        private ProductDTO product;

        public ProductControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();

            product = new ProductDTO
            {
                Name = "Test Product",
                Description = "New Test Product",
                Category = "Testing Only",
                Price = 100
            };
        }

        [Fact]
        public async Task fetching_product_should_not_return_empty()
        {
            var response = await _client.GetAsync("/api/products");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);

            products.Should().NotBeEmpty();
        }

        [Fact]
        public async Task create_new_product_then_delete_it_should_succeed()
        {
            var response = await _client.PostAsync($"/api/products", GetPayLoad(product));
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);

            //var returnedProduct = JsonConvert.DeserializeObject(response.Content.ToString());
            var result = await response.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<ProductDTO>(result);

            response.Headers.Location.ToString().Should().BeEquivalentTo($"/api/products/{product.Id}");

            // Continue with deletion test here 
            delete_a_product_should_succeed();

        }
       
        private async Task delete_a_product_should_succeed()
        {
            var response = await _client.DeleteAsync($"/api/products/{product.Id}");
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }

        private static StringContent GetPayLoad(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
