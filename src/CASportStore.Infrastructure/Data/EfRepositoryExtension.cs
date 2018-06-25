using CASportStore.Core.Entities;
using CASportStore.Core.Exceptions;
using CASportStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CASportStore.Infrastructure.Data
{
    public static class EfRepositoryExtension
    {
        //public static async Task<Product> GetOrFailAsync(this IRepository<Product> repository, int id)
        //{
        //    var product = await repository.GetByIdAsync(id);

        //    if (product ==null)
        //    {
        //        throw new CAException(StatusCode.PRODUCT_NOT_FOUND, $"Product with Id={id} not found.");
        //    }

        //    return product;
        //}
    }
}
