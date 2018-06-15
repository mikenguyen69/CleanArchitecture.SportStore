using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASportStore.Web.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
