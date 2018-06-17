using Microsoft.AspNetCore.Mvc;

namespace CASportStore.Web.Controllers
{
    public class ErrorController : Controller
    {        
        public IActionResult Index() => View();        
    }
}
