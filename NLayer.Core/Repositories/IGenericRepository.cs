using System.Linq.Expressions;

namespace NLayer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    void Update(T entity);
    void Remove(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void RemoveRange(IEnumerable<T> entities);
}