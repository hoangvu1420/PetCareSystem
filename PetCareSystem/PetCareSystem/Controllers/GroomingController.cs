using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.GrommingDtos;
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Controllers;

[Route("api/grooming-services")]
[ApiController]
[Authorize]
public class GroomingController(IGroomingService groomingService) : ControllerBase
{
	private ApiResponse _response = new();

	[HttpGet]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetGroomingServices()
	{
		try
		{
			_response = await groomingService.GetGroomingServicesAsync();
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

	[HttpGet("{groomServiceId:int}", Name = "GetGroomingServiceById")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetGroomingServiceById(int groomServiceId)
	{
		try
		{
			_response = await groomingService.GetGroomingServiceByIdAsync(groomServiceId);
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
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<ApiResponse>> CreateGroomingService(
		[FromBody] CreateGroomingServiceDto groomingServiceDto)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				_response.IsSucceed = false;
				_response.ErrorMessages = errorMessages;
				return BadRequest(_response);
			}

			_response = await groomingService.CreateGroomingServiceAsync(groomingServiceDto);
			var createdGroomingServiceId = (_response.Data as GroomingServiceDto)!.Id;

			return CreatedAtRoute(
				"GetGroomingServiceById", 
				new { groomServiceId = createdGroomingServiceId },
				_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpDelete("{groomServiceId:int}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<ApiResponse>> DeleteGroomingService(int groomServiceId)
	{
		try
		{
			_response = await groomingService.DeleteGroomingServiceAsync(groomServiceId);
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

	[HttpPut("{groomServiceId:int}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<ApiResponse>> UpdateGroomingService(
		int groomServiceId, [FromBody] UpdateGroomingServiceDto updateGroomingServiceDto)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				_response.IsSucceed = false;
				_response.ErrorMessages = errorMessages;
				return BadRequest(_response);
			}

			_response = await groomingService.UpdateGroomingServiceAsync(groomServiceId, updateGroomingServiceDto);
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