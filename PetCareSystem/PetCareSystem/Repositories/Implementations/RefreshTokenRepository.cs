using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class RefreshTokenRepository(ApplicationDbContext dbContext)
	: Repository<RefreshToken>(dbContext), IRefreshTokenRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public async Task SetRevoked(RefreshToken refreshToken)
	{
		refreshToken.IsRevoked = true;
		await _dbContext1.SaveChangesAsync();
	}
}