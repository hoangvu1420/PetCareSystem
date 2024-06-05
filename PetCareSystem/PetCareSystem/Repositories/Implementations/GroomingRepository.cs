using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class GroomingRepository(ApplicationDbContext dbContext)
	: Repository<GroomingService>(dbContext), IGroomingRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public async Task<int> GetBookedCountAsync(int groomingServiceId)
	{
		var count = await _dbContext1.PetGroomingServices.CountAsync(pgs => pgs.GroomingServiceId == groomingServiceId);

		return count;
	}

	public async Task<GroomingService?> UpdateAsync(GroomingService groomingService)
	{
		var groomingServiceToUpdate = await _dbContext1.GroomingServices.FindAsync(groomingService.Id);

		if (groomingServiceToUpdate == null) return groomingServiceToUpdate;

		groomingServiceToUpdate.Name = groomingService.Name;
		groomingServiceToUpdate.Description = groomingService.Description;
		groomingServiceToUpdate.Price = groomingService.Price;

		await _dbContext1.SaveChangesAsync();
		return groomingServiceToUpdate;
	}
}