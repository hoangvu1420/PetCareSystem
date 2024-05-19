using PetCareSystem.DTOs;
using PetCareSystem.DTOs.MedicalReportDtos;
using PetCareSystem.Models;

namespace PetCareSystem.Services.Contracts;

public interface IMedicalRecordService
{
	Task<ApiResponse> GetMedicalRecordsByPetIdAsync(int petId);
	Task<ApiResponse> GetMedicalRecordByRecordIdAsync(int petId, int recordId);
	Task<ApiResponse> CreateMedicalRecordAsync(CreateMedicalRecordDto medicalRecordDto);
	Task<ApiResponse> DeleteMedicalRecordAsync(int petId, int recordId);
	Task<ApiResponse> UpdateMedicalRecordAsync(int petId, int recordId, UpdateMedicalRecordDto updateMedicalRecordDto);
}