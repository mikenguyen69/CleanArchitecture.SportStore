//using CASportStore.Api;
//using CASportStore.Core.DTO;
//using FluentAssertions;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace CASportStore.Tests.Integration.Api.Controllers
//{
//    public class ProductControllerTests
//    {
//        private readonly TestServer _server;
//        private readonly HttpClient _client;

//        public ProductControllerTests()
//        {
//            _server = new TestServer(new WebHostBuilder()
//                .UseStartup<Startup>());
//            _client = _server.CreateClient();
//        }

//        [Fact]
//        public async Task fetching_product_should_not_return_empty()
//        {
//            var response = await _client.GetAsync("api/products");
//            var content = await response.Content.ReadAsStringAsync();
//            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);

//            products.Should().NotBeEmpty();
//        }
//    }
//}
