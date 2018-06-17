using CASportStore.Core.Entities;
using CASportStore.Core.Services;
using CASportStore.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Web.Models
{
    /// <summary>
    /// Provide the Cart the ISession so that it can store themselves. 
    /// </summary>
    public class SessionCart : CartService
    {
        public static CartService GetCart(IServiceProvider services)
        {
            // IHttpContextAccessor service provides access to HttpContext object --> provide the ISession
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.SetJson("Cart", this);
        }
    }
}
