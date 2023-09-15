using BtcDemo.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BtcDemo.Data.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()
{

	protected readonly DbContext _context;

	public RepositoryBase(DbContext context)
	{
		_context = context;
	}

	public async Task<TEntity> GetAsync(IList<Expression<Func<TEntity, bool>>> predicates)
	{
		IQueryable<TEntity> query = _context.Set<TEntity>();

		if (predicates != null && predicates.Any())
		{
			foreach (var predicate in predicates)
			{
				query = query.Where(predicate);
			}
		}
		//AsNoTracking fonksiyonu ile takibi kırılmış tüm nesneler doğal olarak güncelleme durumlarında “SaveChanges” fonksiyonundan etkilenmeyecektirler.
		return await query.AsNoTracking().SingleOrDefaultAsync();
	}

	public async Task<IList<TEntity>> GetAllAsync()
	{
		IQueryable<TEntity> query = _context.Set<TEntity>();

		return await query.AsNoTracking().ToListAsync();
	}

	public async Task<TEntity> AddAsync(TEntity entity)
	{
		await _context.Set<TEntity>().AddAsync(entity);
		return entity;
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		//_context.Entry(entity).State = EntityState.Modified;
		await Task.Run(() => {
			_context.Set<TEntity>().Update(entity);
		});

		return entity;
	}

	public async Task DeleteAsync(TEntity entity)
	{
		// asyn olmadığı için Task.Run içine aldık
		await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
	}

	public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
	{
		return await _context.Set<TEntity>().AnyAsync(predicate);
	}

	public async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
	{
		IQueryable<TEntity> query = _context.Set<TEntity>();

		if (predicate != null)
		{
			query = query.Where(predicate);
		}

		return await query.ToListAsync();
	}


}
