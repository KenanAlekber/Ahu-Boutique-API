using Ahu.Core.Entities.Common;
using System.Linq.Expressions;

namespace Ahu.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SoftDelete(T entity);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<int> SaveAsync();
}