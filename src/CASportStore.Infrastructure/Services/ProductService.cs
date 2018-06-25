using CASportStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CASportStore.Core.DTO;
using System.Threading.Tasks;
using AutoMapper;
using CASportStore.Core.Entities;

namespace CASportStore.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;
        private readonly IMapper mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task AddAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
