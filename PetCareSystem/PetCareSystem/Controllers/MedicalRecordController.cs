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
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Controllers;

[Route("api/medical-record")]
[Authorize]
[ApiController]
public class MedicalRecordController(IMedicalRecordService medicalRecordService) : ControllerBase
{
	private ApiResponse _response = new();

	[HttpGet("petId/{petId:int}")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetMedicalRecordsByPetId(int petId)
	{
		try
		{
			_response = await medicalRecordService.GetMedicalRecordsByPetIdAsync(petId);
			if (!_response.IsSucceed)
				return NotFound(_response);

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