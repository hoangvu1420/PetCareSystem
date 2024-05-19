using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.Models;
using PetCareSystem.CustomFilters;
using PetCareSystem.Services.Contracts;
using PetCareSystem.DTOs.AuthDtos;
using PetCareSystem.DTOs.MedicalReportDtos;
using PetCareSystem.StaticDetails;

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

	[HttpGet("petId/{petId:int}/recordId/{recordId:int}", Name = "GetMedicalRecordByRecordId")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetMedicalRecordByRecordId(int petId, int recordId)
	{
		try
		{
			_response = await medicalRecordService.GetMedicalRecordByRecordIdAsync(petId, recordId);
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

	[HttpPost]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> CreateMedicalRecord([FromBody] CreateMedicalRecordDto medicalRecordDto)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				return BadRequest(new AuthResponse
				{
					IsSucceed = false,
					ErrorMessages = errorMessages
				});
			}

			_response = await medicalRecordService.CreateMedicalRecordAsync(medicalRecordDto);
			if (!_response.IsSucceed)
				return BadRequest(_response);

			var createdRecord = (_response.Data as MedicalRecordDto)!;
			var createdPetId = createdRecord.PetId;
			var createdRecordId = createdRecord.Id;

			return CreatedAtRoute(nameof(GetMedicalRecordByRecordId),
				new { petId = createdPetId, recordId = createdRecordId },
				_response); // return 201 status code with the created medical record
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpDelete("petId/{petId:int}/recordId/{recordId:int}")]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> DeleteMedicalRecord(int petId, int recordId)
	{
		try
		{
			_response = await medicalRecordService.DeleteMedicalRecordAsync(petId, recordId);
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

	[HttpPut("petId/{petId:int}/recordId/{recordId:int}")]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> UpdateMedicalRecord(int petId, int recordId,
		[FromBody] UpdateMedicalRecordDto medicalRecordDto)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				return BadRequest(new AuthResponse
				{
					IsSucceed = false,
					ErrorMessages = errorMessages
				});
			}

			_response = await medicalRecordService.UpdateMedicalRecordAsync(petId, recordId, medicalRecordDto);
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