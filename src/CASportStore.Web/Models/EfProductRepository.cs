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

        public Product Delete(int productId)
        {
            Product dbProduct = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (dbProduct != null)
            {
                _context.Products.Remove(dbProduct);
                _context.SaveChanges();
            }

            return dbProduct;
        }

        public void Save(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products
                    .FirstOrDefault(p => p.Id == product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
