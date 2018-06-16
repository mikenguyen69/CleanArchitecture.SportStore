using System.Linq;

namespace CASportStore.Web.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void Save(Order order);
    }
}
