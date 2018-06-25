using CASportStore.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CASportStore.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAsync();
        Task AddAsync(ProductDTO product);
        Task RemoveAsync(ProductDTO product);
    }
}
