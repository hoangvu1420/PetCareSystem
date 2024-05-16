using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IPetRepository : IRepository<Pet>
{
	Task<Pet?> UpdateAsync(Pet pet);
}