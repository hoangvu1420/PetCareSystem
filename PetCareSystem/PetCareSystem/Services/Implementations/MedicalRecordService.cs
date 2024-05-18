using Microsoft.AspNetCore.Identity;
using PetCareSystem.DTOs;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Contracts;
using PetCareSystem.Utilities;

namespace PetCareSystem.Services.Implementations;

public class MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IPetRepository petRepository) : IMedicalRecordService
{
	public async Task<ApiResponse> GetMedicalRecordsByPetIdAsync(int petId)
	{
		var response = new ApiResponse();

		var isPetExists = await petRepository.ExistsAsync(p => p.Id == petId);
		if (!isPetExists)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var medicalRecords = await medicalRecordRepository.GetAllAsync(filter: mr => mr.PetId == petId);

		var records = medicalRecords.ToList();
		if (!records.Any())
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["No medical records found"];
			return response;
		}

		response.IsSucceed = true;
		response.Data = records.ToMedicalRecordDtoList();
		return response;
	}
}