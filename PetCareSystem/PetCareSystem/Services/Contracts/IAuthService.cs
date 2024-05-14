using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs;

namespace PetCareSystem.Services.Contracts;

public interface IAuthService
{
	Task<AuthResponse> RegisterAsync(RegisterRequestDto request);
	Task<AuthResponse> LoginAsync(LoginRequestDto request);
	Task<AuthResponse> AddToRoleAsync(AddToRoleDto request);
}