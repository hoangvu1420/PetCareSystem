using PetCareSystem.DTOs.GroomingServiceBookingDtos;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.RoomBookingDtos;

namespace PetCareSystem.Services.Contracts;

public interface IRoomBookingService
{
	Task<ApiResponse> GetRoomBookingsAsync();
	Task<ApiResponse> GetRoomBookingsByUserIdAsync(string userId);
	Task<ApiResponse> GetRoomBookingByIdAsync(int bookingId);
	Task<ApiResponse> CreateRoomBookingAsync(CreateRoomBookingDto bookingDto);
	Task<ApiResponse> UpdateRoomBookingAsync(int bookingId, UpdateRoomBookingDto updateBookingDto);
	Task<ApiResponse> DeleteRoomBookingAsync(int bookingId);
}