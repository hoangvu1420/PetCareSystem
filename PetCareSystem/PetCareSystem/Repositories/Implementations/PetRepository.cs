using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class PetRepository(ApplicationDbContext dbContext) : Repository<Pet>(dbContext), IPetRepository
{
	public async Task<Pet?> UpdateAsync(Pet pet)
	{
		// pet.UpdatedAt = DateTime.Now;
		// dbContext.Pets.Update(pet);
		// await dbContext.SaveChangesAsync();
		// return pet;

		var petToUpdate = await dbContext.Pets.FindAsync(pet.Id);

		if (petToUpdate == null) return petToUpdate;

		petToUpdate.Name = pet.Name;
		petToUpdate.Age = pet.Age;
		petToUpdate.HairColor = pet.HairColor;
		petToUpdate.Species = pet.Species;
		petToUpdate.Breed = pet.Breed;
		petToUpdate.ImageUrl = pet.ImageUrl;
		petToUpdate.UpdatedAt = DateTime.Now;

		await dbContext.SaveChangesAsync();
		return petToUpdate;
	}
}