﻿using PetCareSystem.DTOs;
using PetCareSystem.DTOs.MedicalReportDtos;
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

	public async Task<ApiResponse> GetMedicalRecordByRecordIdAsync(int petId, int recordId)
	{
		var response = new ApiResponse();

		var isPetExists = await petRepository.ExistsAsync(p => p.Id == petId);
		if (!isPetExists)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var medicalRecord = await medicalRecordRepository.GetAsync(filter: mr => mr.PetId == petId && mr.Id == recordId);
		if (medicalRecord == null)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Medical record not found"];
			return response;
		}

		response.IsSucceed = true;
		response.Data = medicalRecord.ToMedicalRecordDto();
		return response;
	}

	public async Task<ApiResponse> CreateMedicalRecordAsync(CreateMedicalRecordDto medicalRecordDto)
	{
		var response = new ApiResponse();

		var medicalRecord = medicalRecordDto.ToMedicalRecord();

		await medicalRecordRepository.CreateAsync(medicalRecord);

		response.IsSucceed = true;
		response.Data = medicalRecord.ToMedicalRecordDto();

		return response;
	}

	public async Task<ApiResponse> DeleteMedicalRecordAsync(int petId, int recordId)
	{
		var response = new ApiResponse();

		var isPetExists = await petRepository.ExistsAsync(p => p.Id == petId);
		if (!isPetExists)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var isMedicalReportOfPet = await medicalRecordRepository.ExistsAsync(mr => mr.PetId == petId && mr.Id == recordId);
		if (!isMedicalReportOfPet)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Medical record not found"];
			return response;
		}

		await medicalRecordRepository.DeleteAsync(recordId);

		response.IsSucceed = true;

		return response;
	}

	public async Task<ApiResponse> UpdateMedicalRecordAsync(int petId, int recordId, UpdateMedicalRecordDto updateMedicalRecordDto)
	{
		var response = new ApiResponse();

		if(updateMedicalRecordDto.Id != recordId)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["recordId in the URL and in the request body do not match"];
			return response;
		}

		if(updateMedicalRecordDto.PetId != petId)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["petId in the URL and in the request body do not match"];
			return response;
		}

		var isPetExists = await petRepository.ExistsAsync(p => p.Id == petId);
		if (!isPetExists)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Pet not found"];
			return response;
		}

		var isMedicalReportOfPet = await medicalRecordRepository.ExistsAsync(mr => mr.PetId == petId && mr.Id == recordId);
		if (!isMedicalReportOfPet)
		{
			response.IsSucceed = false;
			response.ErrorMessages = ["Medical record not found"];
			return response;
		}

		var medicalRecordToUpdate = updateMedicalRecordDto.ToMedicalRecord();

		await medicalRecordRepository.UpdateAsync(medicalRecordToUpdate);

		return response;
	}
}