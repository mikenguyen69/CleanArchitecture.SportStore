using CASportStore.Core.Entities;
using CASportStore.Infrastructure.Data;

namespace CASportStore.Api
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            var toDos = dbContext.ToDoItems;
            foreach (var item in toDos)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(new ToDoItem()
            {
                Title = "Test Item 1",
                Description = "Test Description One"
            });
            dbContext.ToDoItems.Add(new ToDoItem()
            {
                Title = "Test Item 2",
                Description = "Test Description Two"
            });
            dbContext.SaveChanges();
        }

    }
}
