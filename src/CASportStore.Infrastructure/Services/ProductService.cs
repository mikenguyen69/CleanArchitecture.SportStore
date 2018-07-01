using CASportStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CASportStore.Core.DTO;
using System.Threading.Tasks;
using AutoMapper;
using CASportStore.Core.Entities;
using CASportStore.Core.Exceptions;
using System.Linq;

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

        public async Task AddAsync(ProductDTO productDTO)
        {
            var product = await repository.GetByIdAsync(productDTO.Id);

            if (product != null)
            {
                throw new CAException(StatusCode.PRODUCT_EXIST, $"Product {productDTO.Description} already exists.");
            }

            product = mapper.Map<ProductDTO, Product>(productDTO);

            product = await repository.AddAsync(product);

            productDTO.Id = product.Id;
        }

        public async Task<IEnumerable<ProductDTO>> GetAsync()
        {
            var products = await repository.ListAsync();

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null)
            {
                throw new CAException(StatusCode.PRODUCT_NOT_FOUND, $"Product with Id={id} not found.");
            }

            return mapper.Map<Product, ProductDTO>(product);
        }

        public async Task RemoveAsync(int id) =>
            await repository.DeleteAsync(id);
    }
}
