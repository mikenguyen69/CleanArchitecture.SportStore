﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CASportStore.Core.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}