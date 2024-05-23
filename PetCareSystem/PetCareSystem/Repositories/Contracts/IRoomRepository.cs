using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IRoomRepository : IRepository<Room>
{
	Task<int> GetBookedCountAsync();
	Task<Room?> UpdateAsync(Room room);
}