using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;

namespace PetCareSystem.Services.Contracts;

public interface IMedicalRecordService
{
	Task<ApiResponse> GetMedicalRecordsByPetIdAsync(int petId);
}