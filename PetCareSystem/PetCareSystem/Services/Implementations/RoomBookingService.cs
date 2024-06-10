using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.RoomBookingDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class RoomBookingService(
	IPetRoomRepository petRoomRepository,
	IPetRepository petRepository,
	IRoomRepository roomRepository,
	UserManager<AppUser> userManager) : IRoomBookingService
{
	public async Task<ApiResponse> GetRoomBookingsAsync()
	{
		var response = new ApiResponse();

		var roomBookings = await petRoomRepository.GetAllAsync(includeProperties: "Pet, Room");

		var roomBookingList = roomBookings.ToList();
		if (!roomBookingList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No room bookings found"];
			return response;
		}

		var roomBookingDtoList = roomBookingList
			.Select(booking => booking.ToRoomBookingDto(
				booking.Pet.Name!,
				booking.Room.Name!))
			.ToList();

		response.IsSucceed = true;
		response.Data = roomBookingDtoList;

		return response;
	}

	public async Task<ApiResponse> GetRoomBookingsByUserIdAsync(string userId)
	{
		var response = new ApiResponse();

		var isUserExist = await userManager.FindByIdAsync(userId) != null;
		if (!isUserExist)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["User not found"];
			return response;
		}

		var petList = await petRepository.GetAllAsync(filter: p => p.OwnerId == userId,
			includeProperties: "PetRooms");
		var roomList = await roomRepository.GetAllAsync();

		var roomBookingList = petList.SelectMany(p => p.PetRooms).ToList();
		if (!roomBookingList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No room bookings found"];
			return response;
		}

		var roomBookingDtoList = roomBookingList
			.Select(booking => booking.ToRoomBookingDto(
				booking.Pet.Name!,
				booking.Room.Name!))
			.ToList();

		response.IsSucceed = true;
		response.Data = roomBookingDtoList;

		return response;
	}

	public async Task<ApiResponse> GetRoomBookingByIdAsync(int bookingId)
	{
		var response = new ApiResponse();

		if (!await petRoomRepository.ExistsAsync(pr => pr.Id == bookingId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room booking not found"];
			return response;
		}

		var roomBooking =
			await petRoomRepository.GetAsync(filter: pr => pr.Id == bookingId, includeProperties: "Pet, Room");

		var roomBookingDto = roomBooking!.ToRoomBookingDto(
			roomBooking!.Pet.Name,
			roomBooking.Room.Name);

		response.IsSucceed = true;
		response.Data = roomBookingDto;

		return response;
	}

	public async Task<ApiResponse> CreateRoomBookingAsync(CreateRoomBookingDto bookingDto)
	{
		var response = new ApiResponse();

		var pet = await petRepository.GetAsync(filter: p => p.Id == bookingDto.PetId);
		if (pet == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var room = await roomRepository.GetAsync(filter: r => r.Id == bookingDto.RoomId);
		if (room == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room not found"];
			return response;
		}

		var existingBookings = await petRoomRepository.GetAllAsync(filter: pr => pr.RoomId == bookingDto.RoomId);
		if (existingBookings.Any(eb => eb.CheckIn < bookingDto.CheckOut && eb.CheckOut > bookingDto.CheckIn))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["The room is already booked for the selected dates"];
			return response;
		}

		var totalPrice = room.Price * (bookingDto.CheckOut - bookingDto.CheckIn).Days;

		var roomBooking = bookingDto.ToPetRoom(totalPrice);

		await petRoomRepository.CreateAsync(roomBooking);

		response.IsSucceed = true;
		response.Data = roomBooking.ToRoomBookingDto(pet.Name, room.Name);

		return response;
	}

	public async Task<ApiResponse> UpdateRoomBookingAsync(int bookingId, UpdateRoomBookingDto updateBookingDto)
	{
		var response = new ApiResponse();

		if (updateBookingDto.Id != bookingId)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["bookingId in the URL and in the request body do not match"];
			return response;
		}

		if (!await petRoomRepository.ExistsAsync(pr => pr.Id == bookingId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room booking not found"];
			return response;
		}

		var bookingToUpdate = await petRoomRepository.GetAsync(filter: pr => pr.Id == bookingId, includeProperties: "Pet, Room");

		var updatedBooking = updateBookingDto.ToPetRoom(bookingToUpdate!.Room.Price);
		await petRoomRepository.UpdateAsync(updatedBooking);

		response.IsSucceed = true;
		response.Data = bookingToUpdate.ToRoomBookingDto(bookingToUpdate.Pet.Name, bookingToUpdate.Room.Name);

		return response;
	}

	public async Task<ApiResponse> DeleteRoomBookingAsync(int bookingId)
	{
		var response = new ApiResponse();

		if (!petRoomRepository.ExistsAsync(pr => pr.Id == bookingId).Result)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Room booking not found"];
			return response;
		}

		await petRoomRepository.DeleteAsync(bookingId);

		response.IsSucceed = true;

		return response;
	}
}