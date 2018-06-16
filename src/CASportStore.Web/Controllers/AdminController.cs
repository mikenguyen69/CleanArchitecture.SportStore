using CASportStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CASportStore.Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId) =>
            View(_repository.Products.FirstOrDefault(x => x.Id == productId));
    }
}
