using System.Linq.Expressions;

namespace PetCareSystem.Repositories.Contracts;

public interface IRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string ? includeProperties = null);
	Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
	Task CreateAsync(T entity);
	Task DeleteAsync(int id);
	Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
	Task SaveAsync();
}