using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Repositories.Implementations;
using PetCareSystem.StaticDetails;
using System.Security.Claims;
using PetCareSystem.Utilities;
using PetCareSystem.CustomFilters;

namespace PetCareSystem.Controllers;

[Route("api/medical-report")]
[Authorize]
[ApiController]
public class MedicalRecordController(IMedicalRecordRepository medicalRecordRepository, UserManager<AppUser> userManager) : ControllerBase
{
	private readonly ApiResponse _response = new();

	[HttpGet("petId/{petId:int}")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetMedicalRecordsByPetId(int petId)
	{
		try
		{
			var isMedicalRecordExists = await medicalRecordRepository.ExistsAsync(mr => mr.PetId == petId);
			if (!isMedicalRecordExists)
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["No medical records found"];
				return NotFound(_response);
			}

			var medicalRecords = await medicalRecordRepository.GetAllAsync(filter: mr => mr.PetId == petId);

			var records = medicalRecords.ToList();
			if (!records.Any())
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["No pets found"];
				return NotFound(_response);
			}

			_response.IsSucceed = true;
			_response.Data = records.ToMedicalRecordDtoList();

			return Ok(_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}
}