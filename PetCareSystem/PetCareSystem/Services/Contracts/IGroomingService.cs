using PetCareSystem.DTOs;
using PetCareSystem.DTOs.GrommingDtos;

namespace PetCareSystem.Services.Contracts;

public interface IGroomingService
{
	Task<ApiResponse> GetGroomingServicesAsync();
	Task<ApiResponse> GetGroomingServiceByIdAsync(int groomServiceId);
	Task<ApiResponse> CreateGroomingServiceAsync(CreateGroomingServiceDto groomingServiceDto);
	Task<ApiResponse> DeleteGroomingServiceAsync(int groomServiceId);
	Task<ApiResponse> UpdateGroomingServiceAsync(int groomServiceId, UpdateGroomingServiceDto groomingServiceDto);
}