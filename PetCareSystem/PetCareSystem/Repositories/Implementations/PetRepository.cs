using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class PetRepository(ApplicationDbContext dbContext) : Repository<Pet>(dbContext), IPetRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public async Task<Pet?> UpdateAsync(Pet pet)
	{
		var petToUpdate = await _dbContext1.Pets.FindAsync(pet.Id);

		if (petToUpdate == null) return petToUpdate;

		petToUpdate.Name = pet.Name;
		petToUpdate.Age = pet.Age;
		petToUpdate.Gender = pet.Gender;
		petToUpdate.HairColor = pet.HairColor;
		petToUpdate.Species = pet.Species;
		petToUpdate.Breed = pet.Breed;
		petToUpdate.ImageUrl = pet.ImageUrl;
		petToUpdate.UpdatedAt = DateTime.Now;

		await _dbContext1.SaveChangesAsync();
		return petToUpdate;
	}
}