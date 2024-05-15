using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs.AuthDtos;

namespace PetCareSystem.Services.Contracts;

public interface IAuthService
{
	Task<AuthResponse> RegisterAsync(RegisterRequestDto request);
	Task<LoginResponse> LoginAsync(LoginRequestDto request);
	Task<AuthResponse> AddToRoleAsync(AddToRoleDto request);
}