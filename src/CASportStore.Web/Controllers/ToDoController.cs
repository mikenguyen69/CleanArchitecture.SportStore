//using CASportStore.Core;
//using CASportStore.Core.Entities;
//using CASportStore.Core.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace CASportStore.Web.Controllers
//{
//    public class ToDoController : Controller
//    {
//        private readonly IRepository<ToDoItem> _todoRepository;

//        public ToDoController(IRepository<ToDoItem> todoRepository)
//        {
//            _todoRepository = todoRepository;
//        }

//        public IActionResult Index()
//        {
//            var items = _todoRepository.List();
//            return View(items);
//        }

//        public IActionResult Populate()
//        {
//            int recordsAdded = DatabasePopulator.PopulateDatabase(_todoRepository);
//            return Ok(recordsAdded);
//        }
//    }
//}
