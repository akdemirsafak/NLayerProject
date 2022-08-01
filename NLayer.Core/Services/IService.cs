using System.Linq.Expressions;

namespace NLayer.Core.Services;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}