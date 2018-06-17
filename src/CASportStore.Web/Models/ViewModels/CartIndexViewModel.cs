using CASportStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Web.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public CartService CartService { get; set; }
        public string ReturnUrl { get; set; }
    }
}
