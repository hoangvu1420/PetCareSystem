using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IGroomingRepository : IRepository<GroomingService>
{
	Task<int> GetBookedCountAsync(int groomingServiceId);
	Task<GroomingService?> UpdateAsync(GroomingService groomingServiceToUpdate);
}