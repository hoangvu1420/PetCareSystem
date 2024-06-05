using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class PetGroomingServiceRepository(ApplicationDbContext dbContext)
	: Repository<PetGroomingService>(dbContext), IPetGroomingServiceRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public async Task<PetGroomingService?> UpdateAsync(PetGroomingService petGroomingService)
	{
		var petGroomingServiceToUpdate = await _dbContext1.PetGroomingServices.FindAsync(petGroomingService.Id);

		if (petGroomingServiceToUpdate == null)
			return null;

		petGroomingServiceToUpdate.Date = petGroomingService.Date;
		petGroomingServiceToUpdate.Notes = petGroomingService.Notes;
		petGroomingServiceToUpdate.UpdatedAt = DateTime.Now;

		await _dbContext1.SaveChangesAsync();

		return petGroomingServiceToUpdate;
	}
}