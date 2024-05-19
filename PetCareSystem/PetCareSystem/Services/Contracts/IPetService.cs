using PetCareSystem.DTOs;
using PetCareSystem.DTOs.PetDtos;

namespace PetCareSystem.Services.Contracts;

public interface IPetService
{
	Task<ApiResponse> GetPetsAsync();
	Task<ApiResponse> GetPetsByUserIdAsync(string userId);
	Task<ApiResponse> GetPetByIdAsync(int petId);
	Task<ApiResponse> CreatePetAsync(CreatePetDto petDto);
	Task<ApiResponse> DeletePetAsync(int petId);
	Task<ApiResponse> UpdatePetAsync(int petId, UpdatePetDto updatePetDto);
}