//using CASportStore.Core;
//using CASportStore.Core.Entities;
//using CASportStore.Core.Interfaces;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace CASportStore.Web.Pages.ToDoRazorPage
//{
//    public class PopulateModel : PageModel
//    {
//        private readonly IRepository<ToDoItem> _todoRepository;

//        public PopulateModel(IRepository<ToDoItem> todoRepository)
//        {
//            _todoRepository = todoRepository;
//        }

//        public int RecordsAdded { get; set; }

//        public void OnGet()
//        {
//            RecordsAdded = DatabasePopulator.PopulateDatabase(_todoRepository);
//        }
//    }
//}
