using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<int> ids);
        Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<string> ids);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<IReadOnlyList<T>> AddRangeAsync(IReadOnlyList<T> entityCollection);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> Exists(T entity);
        Task<bool> Exists(int id);
        Task<bool> Exists(string id);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
