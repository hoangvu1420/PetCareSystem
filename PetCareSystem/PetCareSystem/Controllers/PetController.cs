using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.StaticDetails;
using PetCareSystem.Utilities;

namespace PetCareSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PetController(IPetRepository petRepository, UserManager<AppUser> userManager) : ControllerBase
{
	private readonly ApiResponse _response = new();

	[HttpGet]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetAllPets()
	{
		try
		{
			var pets = await petRepository.GetAllAsync();

			if (!pets.Any())
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

	[HttpGet("/user/{userId}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetPetsByUserId(string userId)
	{
		try
		{
			var isUserExist = await userManager.FindByIdAsync(userId) != null;
			if (!isUserExist)
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["User not found"];
				return BadRequest(_response);
			}

			var pets = await petRepository.GetAllAsync(filter: p => p.OwnerId == userId);

			if (!pets.Any())
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

	[HttpGet("{petId:int}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetPetById(int petId)
	{
		try
		{
			if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["Pet not found"];
				return NotFound(_response);
			}

			var pet = await petRepository.GetAsync(filter: p => p.Id == petId);

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

	[HttpPost]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> CreatePet([FromBody] CreatePetDto petDto)
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

			var pet = petDto.ToPet();

			await petRepository.CreateAsync(pet);

			_response.IsSucceed = true;
			_response.Data = pet;

			return CreatedAtRoute(nameof(GetPetById), new { petId = pet.Id }, _response); // return 201 status code with the created pet
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpDelete("{petId:int}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> DeletePet(int petId)
	{
		try
		{
			if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["Pet not found"];
				return NotFound(_response);
			}

			await petRepository.DeleteAsync(petId);

			_response.IsSucceed = true;

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