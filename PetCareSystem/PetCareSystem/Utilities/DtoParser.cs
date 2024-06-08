using Microsoft.IdentityModel.Tokens;
using PetCareSystem.DTOs.GrommingDtos;
using PetCareSystem.DTOs.GroomingServiceBookingDtos;
using PetCareSystem.DTOs.MedicalReportDtos;
using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.DTOs.RoomBookingDtos;
using PetCareSystem.DTOs.RoomDtos;
using PetCareSystem.Models;
using PetCareSystem.StaticDetails;

namespace PetCareSystem.Utilities;

public static class DtoParser
{
	#region Pet parse methods

	public static Pet ToPet(this CreatePetDto createPetDto)
	{
		string imageUrl;
		if (createPetDto.ImageUrl.IsNullOrEmpty())
		{
			imageUrl = createPetDto.Species switch
			{
				"Dog" => Breeds.GetDogBreed(createPetDto.Breed).ImageUrl,
				"Cat" => Breeds.GetCatBreed(createPetDto.Breed).ImageUrl,
				_ => "null"
			};
		}
		else
		{
			imageUrl = createPetDto.ImageUrl;
		}

		return new Pet
		{
			Name = createPetDto.Name,
			Age = createPetDto.Age,
			Gender = createPetDto.Gender,
			HairColor = createPetDto.HairColor,
			Species = createPetDto.Species,
			Breed = createPetDto.Breed,
			ImageUrl = imageUrl,
			OwnerId = createPetDto.OwnerId
		};
	}

	public static Pet ToPet(this UpdatePetDto updatePetDto)
	{
		return new Pet
		{
			Id = updatePetDto.Id,
			Name = updatePetDto.Name,
			Age = updatePetDto.Age,
			Gender = updatePetDto.Gender,
			HairColor = updatePetDto.HairColor,
			Species = updatePetDto.Species,
			Breed = updatePetDto.Breed,
			ImageUrl = updatePetDto.ImageUrl
		};
	}

	public static PetDto ToPetDto(this Pet pet)
	{
		return new PetDto
		{
			Id = pet.Id,
			Name = pet.Name,
			Age = pet.Age,
			Gender = pet.Gender,
			HairColor = pet.HairColor,
			Species = pet.Species,
			Breed = pet.Breed,
			ImageUrl = pet.ImageUrl,
			OwnerId = pet.OwnerId
		};
	}

	public static IEnumerable<PetDto> ToPetDtoList(this IEnumerable<Pet> pets)
	{
		return pets.Select(pet => pet.ToPetDto()).ToList();
	}

	#endregion


	#region MedicalRecord parse methods

	public static MedicalRecordDto ToMedicalRecordDto(this MedicalRecord medicalRecord)
	{
		return new MedicalRecordDto
		{
			Id = medicalRecord.Id,
			PetId = medicalRecord.PetId,
			Diagnosis = medicalRecord.Diagnosis,
			Date = medicalRecord.Date,
			Doctor = medicalRecord.Doctor,
			Diet = medicalRecord.Diet,
			Medication = medicalRecord.Medication,
			Notes = medicalRecord.Notes,
			NextAppointment = medicalRecord.NextAppointment
		};
	}

	public static IEnumerable<MedicalRecordDto> ToMedicalRecordDtoList(this IEnumerable<MedicalRecord> medicalRecords)
	{
		return medicalRecords.Select(medicalRecord => medicalRecord.ToMedicalRecordDto()).ToList();
	}

	public static MedicalRecord ToMedicalRecord(this CreateMedicalRecordDto createMedicalRecordDto)
	{
		return new MedicalRecord
		{
			PetId = createMedicalRecordDto.PetId,
			Diagnosis = createMedicalRecordDto.Diagnosis,
			Doctor = createMedicalRecordDto.Doctor,
			Diet = createMedicalRecordDto.Diet,
			Medication = createMedicalRecordDto.Medication,
			Notes = createMedicalRecordDto.Notes,
			NextAppointment = createMedicalRecordDto.NextAppointment
		};
	}

	public static MedicalRecord ToMedicalRecord(this UpdateMedicalReportDto updateMedicalRecordDto)
	{
		return new MedicalRecord
		{
			Id = updateMedicalRecordDto.Id,
			PetId = updateMedicalRecordDto.PetId,
			Diagnosis = updateMedicalRecordDto.Diagnosis,
			Doctor = updateMedicalRecordDto.Doctor,
			Diet = updateMedicalRecordDto.Diet,
			Medication = updateMedicalRecordDto.Medication,
			Notes = updateMedicalRecordDto.Notes,
			NextAppointment = updateMedicalRecordDto.NextAppointment
		};
	}

	#endregion


	#region GroomingService parse methods

	public static GroomingServiceDto ToGroomingServiceDto(this GroomingService groomingService, int bookedCount)
	{
		return new GroomingServiceDto
		{
			Id = groomingService.Id,
			Name = groomingService.Name,
			Description = groomingService.Description,
			Price = groomingService.Price,
			BookedCount = bookedCount
		};
	}

	public static GroomingService ToGroomingService(this CreateGroomingServiceDto createGroomingServiceDto)
	{
		return new GroomingService
		{
			Name = createGroomingServiceDto.Name,
			Description = createGroomingServiceDto.Description,
			Price = createGroomingServiceDto.Price
		};
	}

	public static GroomingService ToGroomingService(this UpdateGroomingServiceDto updateGroomingServiceDto)
	{
		return new GroomingService
		{
			Id = updateGroomingServiceDto.Id,
			Name = updateGroomingServiceDto.Name,
			Description = updateGroomingServiceDto.Description,
			Price = updateGroomingServiceDto.Price
		};
	}

	#endregion


	#region Room parse methods

	public static RoomDto ToRoomDto(this Room room)
	{
		return new RoomDto
		{
			Id = room.Id,
			Name = room.Name,
			Price = room.Price,
			Description = room.Description,
			BookedCount = room.PetRooms == null ? 0 : room.PetRooms.Count()
		};
	}

	public static IEnumerable<RoomDto> ToRoomDtoList(this IEnumerable<Room> rooms)
	{
		return rooms.Select(room => room.ToRoomDto()).ToList();
	}

	public static Room ToRoom(this CreateRoomDto createRoomDto)
	{
		return new Room
		{
			Name = createRoomDto.Name,
			Price = createRoomDto.Price,
			Description = createRoomDto.Description
		};
	}

	public static Room ToRoom(this UpdateRoomDto updateRoomDto)
	{
		return new Room
		{
			Id = updateRoomDto.Id,
			Name = updateRoomDto.Name,
			Price = updateRoomDto.Price,
			Description = updateRoomDto.Description
		};
	}

	#endregion


	#region GroomingServiceBooking parse methods

	public static GroomingServiceBookingDto ToGroomingServiceBookingDto(
		this PetGroomingService groomingServiceBooking, string petName, string groomingServiceName)
	{
		return new GroomingServiceBookingDto
		{
			Id = groomingServiceBooking.Id,
			PetId = groomingServiceBooking.PetId,
			GroomingServiceId = groomingServiceBooking.GroomingServiceId,
			PetName = petName,
			GroomingServiceName = groomingServiceName,
			BookingDate = groomingServiceBooking.Date,
			TotalPrice = groomingServiceBooking.TotalPrice,
			Notes = groomingServiceBooking.Notes,
		};
	}

	public static PetGroomingService ToPetGroomingService(
		this CreateGroomingServiceBookingDto createGroomingServiceBookingDto, decimal totalPrice)
	{
		return new PetGroomingService
		{
			PetId = createGroomingServiceBookingDto.PetId,
			GroomingServiceId = createGroomingServiceBookingDto.GroomingServiceId,
			Date = createGroomingServiceBookingDto.BookingDate,
			TotalPrice = totalPrice,
			Notes = createGroomingServiceBookingDto.Notes
		};
	}

	public static PetGroomingService ToPetGroomingService(
		this UpdateGroomingServiceBookingDto updateGroomingServiceBookingDto)
	{
		return new PetGroomingService
		{
			Id = updateGroomingServiceBookingDto.Id,
			Date = updateGroomingServiceBookingDto.BookingDate,
			Notes = updateGroomingServiceBookingDto.Notes
		};
	}

	#endregion

	#region Room booking DTO parser

	public static RoomBookingDto ToRoomBookingDto(this PetRoom petRoom, string petName, string roomName)
	{
		return new RoomBookingDto
		{
			Id = petRoom.Id,
			PetId = petRoom.PetId,
			PetName = petName,
			RoomId = petRoom.RoomId,
			RoomName = roomName,
			CheckIn = petRoom.CheckIn,
			CheckOut = petRoom.CheckOut,
			TotalDays = (petRoom.CheckOut - petRoom.CheckIn).Days,
			TotalPrice = petRoom.TotalPrice,
			Notes = petRoom.Notes
		};
	}

	public static PetRoom ToPetRoom(this CreateRoomBookingDto createRoomBookingDto, decimal totalPrice)
	{
		return new PetRoom
		{
			PetId = createRoomBookingDto.PetId,
			RoomId = createRoomBookingDto.RoomId,
			CheckIn = createRoomBookingDto.CheckIn,
			CheckOut = createRoomBookingDto.CheckOut,
			TotalPrice = totalPrice,
			Notes = createRoomBookingDto.Notes
		};
	}

	public static PetRoom ToPetRoom(this UpdateRoomBookingDto updateRoomBookingDto, decimal price)
	{
		return new PetRoom
		{
			Id = updateRoomBookingDto.Id,
			CheckIn = updateRoomBookingDto.CheckIn,
			CheckOut = updateRoomBookingDto.CheckOut,
			TotalPrice = price * (updateRoomBookingDto.CheckOut - updateRoomBookingDto.CheckIn).Days,
			Notes = updateRoomBookingDto.Notes
		};
	}

	#endregion
}