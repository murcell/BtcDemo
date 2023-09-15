using System.Linq.Expressions;

namespace BtcDemo.Core.Repositories;

public interface IRepository<T> where T : class, new()
{
	Task<T> GetAsync(IList<Expression<Func<T, bool>>> predicates);
	Task<IList<T>> GetAllAsync();
	Task<T> AddAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(T entity);
	Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
	Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
}
