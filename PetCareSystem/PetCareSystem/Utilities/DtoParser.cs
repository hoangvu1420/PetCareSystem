using Microsoft.IdentityModel.Tokens;
using PetCareSystem.DTOs.MedicalReportDtos;
using PetCareSystem.DTOs.PetDtos;
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

	public static MedicalRecord ToMedicalRecord(this UpdateMedicalRecordDto updateMedicalRecordDto)
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

}