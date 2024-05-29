using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
	public Task SetRevoked(RefreshToken refreshToken);
}