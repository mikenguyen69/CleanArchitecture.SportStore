using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CASportStore.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IRepository<Product> _repository;

        public NavigationMenuViewComponent(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.List()
                .Select( x => x.Category)
                .Distinct()
                .OrderBy(x => x)
                );
        }
    }
}
