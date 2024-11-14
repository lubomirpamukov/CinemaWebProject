using CinemaWeb.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CinemaDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(CinemaDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public  IQueryable<T> GetAllAttachedAsync()
    {
        return _dbSet.AsQueryable();
    }

    public Task<T> FindByIdAsync(object id)
    {
        throw new ArgumentException();
    }

    public Task<bool> AddAsync(T entity)
    {
        throw new ArgumentException();
    }

    public Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        throw new ArgumentException();
    }

    public Task<bool> UpdateAsync(T entity)
    {
        throw new ArgumentException();
    }

    public Task<bool> SoftDeleteAsync(T entity)
    {
        throw new ArgumentException();
    }

    public Task<bool> SoftDeleteRangeAsync(IEnumerable<T> entities)
    {
        throw new ArgumentException();
    }

    public Task<bool> DeleteAsync(T entity)
    {
        throw new ArgumentException();
    }

    public Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
    {
        throw new ArgumentException();
    }

    public Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new ArgumentException();
    }

    public Task<bool> ExistsAsync(Func<T, bool> predicate)
    {
        throw new ArgumentException();
    }

    public Task<int> CountAsync()
    {
        throw new ArgumentException();
    }
}
