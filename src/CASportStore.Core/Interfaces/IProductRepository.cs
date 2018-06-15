using CASportStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASportStore.Core.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
