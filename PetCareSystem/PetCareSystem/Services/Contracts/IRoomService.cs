using PetCareSystem.DTOs;
using PetCareSystem.DTOs.RoomDtos;

namespace PetCareSystem.Services.Contracts;

public interface IRoomService
{
	Task<ApiResponse> GetRoomsAsync();
	Task<ApiResponse> GetRoomByIdAsync(int roomId);
	Task<ApiResponse> CreateRoomAsync(CreateRoomDto roomDto);
	Task<ApiResponse> UpdateRoomAsync(int roomId, UpdateRoomDto roomDto);
	Task<ApiResponse> DeleteRoomAsync(int roomId);
}