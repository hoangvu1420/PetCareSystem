using Microsoft.EntityFrameworkCore;
using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class PetRoomRepository(ApplicationDbContext dbContext) : Repository<PetRoom>(dbContext), IPetRoomRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public async Task<PetRoom?> UpdateAsync(PetRoom petRoom)
	{
		var petRoomToUpdate = await _dbContext1.PetRooms.FindAsync(petRoom.Id);

		if (petRoomToUpdate == null)
			return null;

		petRoomToUpdate.CheckIn = petRoom.CheckIn;
		petRoomToUpdate.CheckOut = petRoom.CheckOut;
		petRoomToUpdate.TotalPrice = petRoom.TotalPrice;
		petRoomToUpdate.Notes = petRoom.Notes;
		petRoomToUpdate.UpdatedAt = DateTime.Now;

		await _dbContext1.SaveChangesAsync();

		return petRoomToUpdate;
	}
}