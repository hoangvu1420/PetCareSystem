using PetCareSystem.DTOs;
using PetCareSystem.DTOs.GrommingDtos;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class GroomingService(IGroomingRepository groomingRepository) : IGroomingService
{
	public async Task<ApiResponse> GetGroomingServicesAsync()
	{
		var response = new ApiResponse();

		var groomingServices = await groomingRepository.GetAllAsync();

		var groomingServiceList = groomingServices.ToList();
		if (!groomingServiceList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No grooming services found"];
			return response;
		}

		var groomingServiceDtoList = new List<GroomingServiceDto>();
		foreach (var groomingService in groomingServiceList)
		{
			var bookedCount = await groomingRepository.GetBookedCountAsync(groomingService.Id);
			groomingServiceDtoList.Add(groomingService.ToGroomingServiceDto(bookedCount));
		}

		response.IsSucceed = true;
		response.Data = groomingServiceDtoList;

		return response;
	}

	public async Task<ApiResponse> GetGroomingServiceByIdAsync(int groomingServiceId)
	{
		var response = new ApiResponse();

		var groomingService = await groomingRepository.GetAsync(filter: g => g.Id == groomingServiceId);

		if (groomingService == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service not found"];
			return response;
		}

		var bookedCount = await groomingRepository.GetBookedCountAsync(groomingService.Id);
		var groomingServiceDto = groomingService.ToGroomingServiceDto(bookedCount);

		response.IsSucceed = true;
		response.Data = groomingServiceDto;

		return response;
	}

	public async Task<ApiResponse> CreateGroomingServiceAsync(CreateGroomingServiceDto groomingServiceDto)
	{
		var response = new ApiResponse();

		var groomingService = groomingServiceDto.ToGroomingService();

		await groomingRepository.CreateAsync(groomingService);

		response.IsSucceed = true;
		response.Data = groomingService.ToGroomingServiceDto(0);

		return response;
	}

	public async Task<ApiResponse> DeleteGroomingServiceAsync(int groomingServiceId)
	{
		var response = new ApiResponse();

		var groomingService = await groomingRepository.GetAsync(filter: g => g.Id == groomingServiceId);
		if (groomingService == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service not found"];
			return response;
		}

		await groomingRepository.DeleteAsync(groomingServiceId);

		response.IsSucceed = true;

		return response;
	}

	public async Task<ApiResponse> UpdateGroomingServiceAsync(int groomingServiceId, UpdateGroomingServiceDto groomingServiceDto)
	{
		var response = new ApiResponse();

		if(groomingServiceId != groomingServiceDto.Id)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["groomingServiceId in the URL and in the request body do not match"];
			return response;
		}

		if (!await groomingRepository.ExistsAsync(filter: g => g.Id == groomingServiceId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service not found"];
			return response;
		}

		var groomingServiceToUpdate = groomingServiceDto.ToGroomingService();

		var updatedGroomingService = await groomingRepository.UpdateAsync(groomingServiceToUpdate);
		if (updatedGroomingService == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Failed to update grooming service"];
			return response;
		}
		
		var bookedCount = await groomingRepository.GetBookedCountAsync(updatedGroomingService.Id);

		response.IsSucceed = true;
		response.Data = updatedGroomingService.ToGroomingServiceDto(bookedCount);

		return response;
	}
}