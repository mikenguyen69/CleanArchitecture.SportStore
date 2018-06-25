using System.Collections.Generic;
using CASportStore.Core.SharedKernel;
using System.Threading.Tasks;

namespace CASportStore.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}