using CASportStore.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Core.Entities
{
    public class CartLine : BaseEntity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
