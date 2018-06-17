using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Web.Models;
using CASportStore.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CASportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> _repository;
        public int PageSize = 4;

        // Auto resolve via dependency injection
        public ProductController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        // Call without view name means tell MVC to render default view for action method
        public ViewResult List(string category, int page = 1)
         => View(new ProductsListViewModel
         {
             Products = _repository.List()
             .Where(x => category == null || x.Category == category)
             .OrderBy(p => p.Id)
             .Skip((page - 1) * PageSize)
             .Take(PageSize), 

             PagingInfo = new PagingInfo
             {
                 CurrentPage = page, 
                 ItemsPerPage = PageSize,
                 TotalItems = category == null ? _repository.List().Count() : _repository.List().Count(x => x.Category == category)
             }, 

             CurrentCategory = category
         });  
    }
}
