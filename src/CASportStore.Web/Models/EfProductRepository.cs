using System.Linq;

namespace CASportStore.Web.Models
{
    public class EfProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EfProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
