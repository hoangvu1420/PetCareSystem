using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class PetService(IPetRepository petRepository, UserManager<AppUser> userManager) : IPetService
{
	public async Task<ApiResponse> GetPetsAsync()
	{
		var response = new ApiResponse();

		var pets = await petRepository.GetAllAsync();

		var petList = pets.ToList();
		if (!petList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No pets found."];
			return response;
		}

		response.IsSucceed = true;
		response.Data = petList.ToPetDtoList();

		return response;
	}

	public async Task<ApiResponse> GetPetsByUserIdAsync(string userId)
	{
		var response = new ApiResponse();

		var isUserExist = await userManager.FindByIdAsync(userId) != null;
		if (!isUserExist)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["User not found"];
			return response;
		}

		var pets = await petRepository.GetAllAsync(filter: p => p.OwnerId == userId);

		var petList = pets.ToList();
		if (!petList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No pets found"];
			return response;
		}

		response.IsSucceed = true;
		response.Data = petList.ToPetDtoList();

		return response;
	}

	public async Task<ApiResponse> GetPetByIdAsync(int petId)
	{
		var response = new ApiResponse();

		if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var pet = await petRepository.GetAsync(filter: p => p.Id == petId);

		response.IsSucceed = true;
		response.Data = pet.ToPetDto();

		return response;
	}

	public async Task<ApiResponse> CreatePetAsync(CreatePetDto petDto)
	{
		var response = new ApiResponse();

		var pet = petDto.ToPet();

		await petRepository.CreateAsync(pet);

		response.IsSucceed = true;
		response.Data = pet.ToPetDto();

		return response;
	}

	public async Task<ApiResponse> DeletePetAsync(int petId)
	{
		var response = new ApiResponse();

		if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		await petRepository.DeleteAsync(petId);

		response.IsSucceed = true;

		return response;
	}

	public async Task<ApiResponse> UpdatePetAsync(int petId, UpdatePetDto updatePetDto)
	{
		var response = new ApiResponse();

		if (petId != updatePetDto.Id)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["petId in the URL and in the request body do not match"];
			return response;
		}

		if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var petToUpdate = updatePetDto.ToPet();

		var updatedPet = await petRepository.UpdateAsync(petToUpdate);

		response.IsSucceed = true;
		response.Data = updatedPet.ToPetDto();

		return response;
	}
}