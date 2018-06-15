using CASportStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CASportStore.Core.Entities;
using System.Linq;

namespace CASportStore.Infrastructure.Data
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
