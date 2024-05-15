using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly ApplicationDbContext _dbContext;
	internal DbSet<T> dbSet;

	public Repository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
		dbSet = _dbContext.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties == null)
		{
			return await query.ToListAsync();
		}

		query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

		return await query.ToListAsync();
	}

	public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true,
		string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (!tracked)
		{
			query = query.AsNoTracking();
		}

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties == null)
		{
			return await query.FirstOrDefaultAsync();
		}

		query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

		return await query.FirstOrDefaultAsync();
	}

	public async Task CreateAsync(T entity)
	{
		await dbSet.AddAsync(entity);
		await SaveAsync();
	}

	public async Task DeleteAsync(int id)
	{
		dbSet.Remove(dbSet.Find(id));
		await SaveAsync();
	}

	public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
	{
		return await dbSet.AnyAsync(filter);
	}

	public async Task SaveAsync()
	{
		await _dbContext.SaveChangesAsync();
	}
}