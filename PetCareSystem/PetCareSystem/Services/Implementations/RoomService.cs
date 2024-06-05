using PetCareSystem.DTOs;
using PetCareSystem.DTOs.RoomDtos;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
	public async Task<ApiResponse> GetRoomsAsync()
	{
		var response = new ApiResponse();

		var rooms = await roomRepository.GetAllAsync(includeProperties: "PetRooms");
		var roomList = rooms.ToList();
		if (!roomList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No rooms found"];
			return response;
		}

		response.IsSucceed = true;
		response.Data = roomList.ToRoomDtoList();

		return response;
	}

	public async Task<ApiResponse> GetRoomByIdAsync(int roomId)
	{
		var response = new ApiResponse();

		var room = await roomRepository.GetAsync(filter: r => r.Id == roomId, includeProperties: "PetRooms");
		if (room == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room not found"];
			return response;
		}

		response.IsSucceed = true;
		response.Data = room.ToRoomDto();

		return response;
	}

	public async Task<ApiResponse> CreateRoomAsync(CreateRoomDto roomDto)
	{
		var response = new ApiResponse();

		var room = roomDto.ToRoom();

		await roomRepository.CreateAsync(room);

		response.IsSucceed = true;
		response.Data = room.ToRoomDto();

		return response;
	}

	public async Task<ApiResponse> DeleteRoomAsync(int roomId)
	{
		var response = new ApiResponse();

		var isRoomExist = await roomRepository.ExistsAsync(r => r.Id == roomId);
		if (!isRoomExist)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room not found"];
			return response;
		}

		await roomRepository.DeleteAsync(roomId);

		response.IsSucceed = true;

		return response;
	}

	public async Task<ApiResponse> UpdateRoomAsync(int roomId, UpdateRoomDto roomDto)
	{
		var response = new ApiResponse();

		if (roomId != roomDto.Id)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["roomId in the URL and in the request body do not match"];
			return response;
		}

		if (!await roomRepository.ExistsAsync(r => r.Id == roomId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room not found"];
			return response;
		}

		var roomToUpdate = roomDto.ToRoom();

		var updatedRoom = await roomRepository.UpdateAsync(roomToUpdate);

		response.IsSucceed = true;
		if (updatedRoom != null) response.Data = updatedRoom.ToRoomDto();

		return response;
	}
}