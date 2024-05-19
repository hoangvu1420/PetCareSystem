using PetCareSystem.DTOs.AuthDtos;

namespace PetCareSystem.Services.Contracts;

public interface IAuthService
{
	Task<AuthResponse> RegisterAsync(RegisterRequestDto request);
	Task<LoginResponse> LoginAsync(LoginRequestDto request);
	Task<RefreshTokenResponse> IssueRefreshTokenAsync(RefreshTokenRequestDto request);
	Task<AuthResponse> AddToRoleAsync(AddToRoleDto request);
}