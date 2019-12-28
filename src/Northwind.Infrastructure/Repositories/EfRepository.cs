using Microsoft.EntityFrameworkCore;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly NorthwindDbContext _northwindDbContext;

        public EfRepository(NorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _northwindDbContext.Set<T>().AddAsync(entity);
            await _northwindDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IReadOnlyList<T>> AddRangeAsync(IReadOnlyList<T> entityCollection)
        {
            await _northwindDbContext.Set<T>().AddRangeAsync(entityCollection);
            await _northwindDbContext.SaveChangesAsync();

            return entityCollection;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _northwindDbContext.Set<T>().Remove(entity);
            await _northwindDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _northwindDbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _northwindDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _northwindDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<int> ids)
        {
            var entities = new List<T>();

            foreach (var id in ids)
            {
                var e = await _northwindDbContext.Set<T>().FindAsync(id);

                entities.Add(e);
            }

            return entities;
        }

        public async Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<string> ids)
        {
            var entities = new List<T>();

            foreach (var id in ids)
            {
                var e = await _northwindDbContext.Set<T>().FindAsync(id);

                entities.Add(e);
            }

            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            _northwindDbContext.Entry(entity).State = EntityState.Modified;
            await _northwindDbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(T entity)
        {
            return await _northwindDbContext.Set<T>().AnyAsync(n => n == entity);
        }

        public async Task<bool> Exists(int id)
        {
            return (await _northwindDbContext.Set<T>().FindAsync(id)) != null;
        }

        public async Task<bool> Exists(string id)
        {
            return (await _northwindDbContext.Set<T>().FindAsync(id)) != null;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_northwindDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
