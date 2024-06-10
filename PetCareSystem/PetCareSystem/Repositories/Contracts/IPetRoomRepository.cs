using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IPetRoomRepository : IRepository<PetRoom>
{
	Task<PetRoom?> UpdateAsync(PetRoom petRoom);
}