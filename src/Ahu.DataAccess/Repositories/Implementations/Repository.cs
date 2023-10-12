using Ahu.Core.Entities.Common;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ahu.DataAccess.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _table;

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public void Add(T entity)
        => _table.Add(entity);

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.Where(expression);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task CreateAsync(T entity)
        => _table.AddAsync(entity);

    public void Update(T entity)
        => _table.Update(entity);

    public void Delete(T entity)
        => _table.Remove(entity);

    public void SoftDelete(T entity)
        => entity.IsDeleted = true;

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query.Include(include);
            }
        }

        return await query.AnyAsync(expression);
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}