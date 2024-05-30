using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IPetGroomingServiceRepository : IRepository<PetGroomingService>
{
	Task<PetGroomingService?> UpdateAsync(PetGroomingService petGroomingService);
}