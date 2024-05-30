using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.GroomingServiceBookingDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class GroomingServiceBookingService(
	IPetGroomingServiceRepository petGroomingServiceRepository,
	IPetRepository petRepository,
	IGroomingRepository groomingRepository,
	UserManager<AppUser> userManager) : IGroomingServiceBookingService
{
	public async Task<ApiResponse> GetGroomingServiceBookingsAsync()
	{
		var response = new ApiResponse();

		var groomingServiceBookings =
			await petGroomingServiceRepository.GetAllAsync();

		var groomingServiceBookingList = groomingServiceBookings.ToList();
		if (!groomingServiceBookingList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No grooming service bookings found"];
			return response;
		}

		var pets = await petRepository.GetAllAsync();
		var groomingServices = await groomingRepository.GetAllAsync();

		var groomingServiceBookingDtoList = groomingServiceBookingList
			.Select(booking => booking.ToGroomingServiceBookingDto(
				pets.FirstOrDefault(p => p.Id == booking.PetId)?.Name!,
				groomingServices.FirstOrDefault(g => g.Id == booking.GroomingServiceId)?.Name!))
			.ToList();

		response.IsSucceed = true;
		response.Data = groomingServiceBookingDtoList;

		return response;
	}

	public async Task<ApiResponse> GetGroomingServiceBookingsByUserIdAsync(string userId)
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
			includeProperties: "PetGroomingServices");
		var groomingServiceList = await groomingRepository.GetAllAsync();

		var groomingServiceBookingList = petList.SelectMany(p => p.PetGroomingServices).ToList();
		if (!groomingServiceBookingList.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No grooming service bookings found"];
			return response;
		}

		var groomingServiceBookingDtoList = groomingServiceBookingList
			.Select(booking => booking.ToGroomingServiceBookingDto(booking.Pet.Name,
				groomingServiceList.FirstOrDefault(g => g.Id == booking.GroomingServiceId)?.Name!))
			.ToList();

		response.IsSucceed = true;
		response.Data = groomingServiceBookingDtoList;

		return response;
	}

	public async Task<ApiResponse> GetGroomingServiceBookingByIdAsync(int bookingId)
	{
		var response = new ApiResponse();

		if (!await petGroomingServiceRepository.ExistsAsync(filter: b => b.Id == bookingId))
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service booking not found"];
			return response;
		}

		var groomingServiceBooking = await petGroomingServiceRepository.GetAsync(filter: b => b.Id == bookingId);
		var pet = await petRepository.GetAsync(filter: p => p.Id == groomingServiceBooking!.PetId);
		var groomingService = await groomingRepository.GetAsync(filter: g => g.Id == groomingServiceBooking!.GroomingServiceId);

		var groomingServiceBookingDto = groomingServiceBooking!.ToGroomingServiceBookingDto(
			pet!.Name,
			groomingService!.Name);

		response.IsSucceed = true;
		response.Data = groomingServiceBookingDto;
		return response;
	}

	public async Task<ApiResponse> CreateGroomingServiceBookingAsync(CreateGroomingServiceBookingDto bookingDto)
	{
		var response = new ApiResponse();

		var pet = await petRepository.GetAsync(filter: p => p.Id == bookingDto.PetId);
		if (pet == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var groomingService = await groomingRepository.GetAsync(filter: g => g.Id == bookingDto.GroomingServiceId);
		if (groomingService == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service not found"];
			return response;
		}

		var totalPrice = groomingService.Price;
		var groomingServiceBooking = bookingDto.ToPetGroomingService(totalPrice);

		await petGroomingServiceRepository.CreateAsync(groomingServiceBooking);

		response.IsSucceed = true;
		response.Data = groomingServiceBooking.ToGroomingServiceBookingDto(pet.Name, groomingService.Name);

		return response;
	}

	public async Task<ApiResponse> UpdateGroomingServiceBookingAsync(int bookingId,
		UpdateGroomingServiceBookingDto updateBookingDto)
	{
		var response = new ApiResponse();

		if (updateBookingDto.Id != bookingId)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["bookingId in the URL and in the request body do not match"];
			return response;
		}

		var isBookingExist = await petGroomingServiceRepository.ExistsAsync(b => b.Id == bookingId);
		if (!isBookingExist)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service booking not found"];
			return response;
		}

		var bookingToUpdate = await petGroomingServiceRepository.GetAsync(filter: b => b.Id == bookingId);
		var pet = await petRepository.GetAsync(filter: p => p.Id == bookingToUpdate!.PetId);
		var groomingService = await groomingRepository.GetAsync(filter: g => g.Id == bookingToUpdate!.GroomingServiceId);

		var updatedBooking = updateBookingDto.ToPetGroomingService();
		await petGroomingServiceRepository.UpdateAsync(updatedBooking);

		response.IsSucceed = true;
		response.Data = bookingToUpdate!.ToGroomingServiceBookingDto(
			pet!.Name,
			groomingService!.Name);

		return response;
	}

	public async Task<ApiResponse> DeleteGroomingServiceBookingAsync(int bookingId)
	{
		var response = new ApiResponse();

		var isBookingExist = await petGroomingServiceRepository.ExistsAsync(b => b.Id == bookingId);
		if (!isBookingExist)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Grooming service booking not found"];
			return response;
		}

		await petGroomingServiceRepository.DeleteAsync(bookingId);

		response.IsSucceed = true;

		return response;
	}
}