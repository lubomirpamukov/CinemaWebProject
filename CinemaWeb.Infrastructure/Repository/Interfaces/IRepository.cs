using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Infrastructure.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<IQueryable<T>> GetAllAttachedAsync();

    public Task<T> FindByIdAsync(object id);

    public Task<bool> AddAsync(T entity);

    public Task<bool> AddRangeAsync(IEnumerable<T> entities);

    public Task<bool> UpdateAsync(T entity);

    public Task<bool> SoftDeleteAsync(T entity);

    public Task<bool> SoftDeleteRangeAsync(IEnumerable<T> entities);

    public Task<bool> DeleteAsync(T entity);

    public Task<bool> DeleteRangeAsync(IEnumerable<T> entities);

    public Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);

    public Task<bool> ExistsAsync(Func<T, bool> predicate);

    public Task<int> CountAsync();


}
