using PetCareSystem.DTOs;
using PetCareSystem.DTOs.MedicalReportDtos;
using PetCareSystem.Models;

namespace PetCareSystem.Services.Contracts;

public interface IMedicalRecordService
{
	Task<ApiResponse> GetMedicalRecordsAsync();
	Task<ApiResponse> GetMedicalRecordsByPetIdAsync(int petId);
	Task<ApiResponse> GetMedicalRecordByRecordIdAsync(int recordId);
	Task<ApiResponse> CreateMedicalRecordAsync(CreateMedicalRecordDto medicalRecordDto);
	Task<ApiResponse> DeleteMedicalRecordAsync(int recordId);
	Task<ApiResponse> UpdateMedicalRecordAsync(int recordId, UpdateMedicalReportDto updateMedicalRecordDto);
}