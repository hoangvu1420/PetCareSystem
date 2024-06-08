using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly ApplicationDbContext _dbContext;
	internal DbSet<T> DbSet;

	public Repository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
		DbSet = _dbContext.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
	{
		IQueryable<T> query = DbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties == null)
		{
			return await query.ToListAsync();
		}

		query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(p => p.Trim())  // Add this line to trim whitespace
			.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

		return await query.ToListAsync();
	}

	public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true,
		string? includeProperties = null)
	{
		IQueryable<T> query = DbSet;

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
			.Select(p => p.Trim())  // Add this line to trim whitespace
			.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

		return await query.FirstOrDefaultAsync();
	}

	public async Task CreateAsync(T entity)
	{
		if (entity is BaseEntity baseEntity)
		{
			baseEntity.CreatedAt = DateTime.Now;
			baseEntity.UpdatedAt = DateTime.Now;
		}

		await DbSet.AddAsync(entity);
		await SaveAsync();
	}

	public async Task DeleteAsync(int id)
	{
		DbSet.Remove((await DbSet.FindAsync(id))!);
		await SaveAsync();
	}

	public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
	{
		return await DbSet.AnyAsync(filter);
	}

	public async Task SaveAsync()
	{
		await _dbContext.SaveChangesAsync();
	}
}