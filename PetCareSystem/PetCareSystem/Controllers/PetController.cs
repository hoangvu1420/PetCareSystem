using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PetController(IPetRepository petRepository, UserManager<AppUser> userManager) : ControllerBase
{
	private readonly ApiResponse _response = new();

	[HttpGet("/user/{userId}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetPetsOfUser(string userId)
	{
		try
		{
			var isUserExist = await userManager.FindByIdAsync(userId);
			if (isUserExist == null)
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["User not found"];
				return BadRequest(_response);
			}

			var pets = await petRepository.GetAllAsync(filter: p => p.OwnerId == userId);

			if (pets == null)
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["No pets found"];
				return NotFound(_response);
			}

			_response.IsSucceed = true;
			_response.Data = pets;

			return Ok(_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpGet("{id}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetPetById(int id)
	{
		try
		{
			if (!await petRepository.ExistsAsync(filter: p => p.Id == id))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["Pet not found"];
				return NotFound(_response);
			}

			var pet = await petRepository.GetAsync(filter: p => p.Id == id);

			_response.IsSucceed = true;
			_response.Data = pet;

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