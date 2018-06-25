using AutoMapper;
using CASportStore.Core.DTO;
using CASportStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CASportStore.Core.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
            });

            return config.CreateMapper();
        }
    }
}
