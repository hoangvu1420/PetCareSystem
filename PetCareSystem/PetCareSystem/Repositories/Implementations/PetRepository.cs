using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class PetRepository(ApplicationDbContext dbContext) : Repository<Pet>(dbContext), IPetRepository
{
	
}