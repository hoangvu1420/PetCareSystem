using PetCareSystem.DTOs;
using PetCareSystem.DTOs.GroomingServiceBookingDtos;

namespace PetCareSystem.Services.Contracts;

public interface IGroomingServiceBookingService
{
	Task<ApiResponse> GetGroomingServiceBookingsAsync();
	Task<ApiResponse> GetGroomingServiceBookingsByUserIdAsync(string userId);
	Task<ApiResponse> GetGroomingServiceBookingByIdAsync(int bookingId);
	Task<ApiResponse> CreateGroomingServiceBookingAsync(CreateGroomingServiceBookingDto bookingDto);
	Task<ApiResponse> UpdateGroomingServiceBookingAsync(int bookingId, UpdateGroomingServiceBookingDto updateBookingDto);
	Task<ApiResponse> DeleteGroomingServiceBookingAsync(int bookingId);
}