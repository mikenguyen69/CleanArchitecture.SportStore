using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CASportStore.Web.Models
{
    public class EfOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public EfOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // specify that when an Order object is read from the database, the collection associated with 
        // the Lines property should also be loaded along with each Product object associated with each 
        // collection object.
        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void Save(Order order)
        {
            // An additional step is required when I store an Order object in the database. 
            // When the user’s cart data is deserialized from the session store, the JSON package 
            // creates new objects that are not known to Entity Framework Core, which then tries to 
            // write all the objects into the database. For the Product objects, this means that 
            // Entity Framework Core tries to write objects that have already been stored, which causes an error. 
            // To avoid this problem, I notify Entity Framework Core that the objects exist and 
            // shouldn’t be stored in the database unless they are modified, as follows:
            _context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.Id == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
