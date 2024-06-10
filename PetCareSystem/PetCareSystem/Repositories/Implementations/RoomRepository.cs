using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class RoomRepository(ApplicationDbContext dbContext) : Repository<Room>(dbContext), IRoomRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public Task<int> GetBookedCountAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<Room?> UpdateAsync(Room room)
	{
		var roomToUpdate = await _dbContext1.Rooms.FindAsync(room.Id);

		if (roomToUpdate == null) return roomToUpdate;

		roomToUpdate.Name = room.Name;
		roomToUpdate.Description = room.Description;
		roomToUpdate.Price = room.Price;

		await _dbContext1.SaveChangesAsync();
		return roomToUpdate;
	}
}