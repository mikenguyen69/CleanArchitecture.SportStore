using System.Collections.Generic;
using System.Linq;
using CASportStore.Core.Interfaces;
using CASportStore.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CASportStore.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task<T> GetByIdAsync(int id) =>
            await Task.FromResult(_dbContext.Set<T>().SingleOrDefault(e => e.Id == id));

        public async Task<IEnumerable<T>> ListAsync() =>
            await Task.FromResult(_dbContext.Set<T>().ToList());

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();

            await Task.CompletedTask;
        }        
    }
}