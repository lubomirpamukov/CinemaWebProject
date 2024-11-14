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

    public async Task<T?> FindByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> AddAsync(T entity)
    {
        if (entity == null) 
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        try
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }
    public async Task<bool> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public Task<bool> SoftDeleteAsync(T entity)
    {
        throw new ArgumentException();
    }

    public Task<bool> SoftDeleteRangeAsync(IEnumerable<T> entities)
    {
        throw new ArgumentException();
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        try
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new ArgumentException();
    }

    public async Task<bool> AnyAsync(Func<T, bool> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return await _dbSet.AnyAsync(x => predicate(x));

    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

}
