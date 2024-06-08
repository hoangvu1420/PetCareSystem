using Microsoft.AspNetCore.Mvc;
using PetCareSystem.CustomFilters;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.RoomBookingDtos;
using PetCareSystem.Models;
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Controllers;

[Route("api/room-bookings")]
[ApiController]
public class RoomBookingController(IRoomBookingService roomBookingService) : ControllerBase
{
	private ApiResponse _response = new();

	[HttpGet]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<ApiResponse>> GetBookings([FromQuery] string? userId)
	{
		try
		{
			if (!string.IsNullOrEmpty(userId))
			{
				_response = await roomBookingService.GetRoomBookingsByUserIdAsync(userId);
				if (!_response.IsSucceed)
					return NotFound(_response);

				return Ok(_response);
			}

			_response = await roomBookingService.GetRoomBookingsAsync();
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

	[HttpGet("{bookingId:int}", Name = "GetRoomBookingById")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetBookingById(int bookingId)
	{
		try
		{
			_response = await roomBookingService.GetRoomBookingByIdAsync(bookingId);
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
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> CreateBooking([FromBody] CreateRoomBookingDto bookingDto)
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

			_response = await roomBookingService.CreateRoomBookingAsync(bookingDto);
			if (!_response.IsSucceed)
				return BadRequest(_response);

			var createdBookingId = (_response.Data as RoomBookingDto)!.Id;

			return CreatedAtRoute("GetRoomBookingById", new { bookingId = createdBookingId }, _response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpPut("{bookingId:int}")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> UpdateBooking(int bookingId,
		[FromBody] UpdateRoomBookingDto updateBookingDto)
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

			_response = await roomBookingService.UpdateRoomBookingAsync(bookingId, updateBookingDto);
			if (!_response.IsSucceed)
				return BadRequest(_response);

			return Ok(_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpDelete("{bookingId:int}")]
	[ResourceAuthorize(typeof(Pet))] // Custom filter to authorize access to resources
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> DeleteBooking(int bookingId)
	{
		try
		{
			_response = await roomBookingService.DeleteRoomBookingAsync(bookingId);
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