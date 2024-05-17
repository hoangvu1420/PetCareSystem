using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.StaticDetails;
using PetCareSystem.Utilities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace PetCareSystem.Controllers;

[Route("api/pet")]
[ApiController]
[Authorize]
public class PetController(IPetRepository petRepository, UserManager<AppUser> userManager) : ControllerBase
{
	private readonly ApiResponse _response = new();

	[HttpGet]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
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
			_response.Data = pets.ToPetDtoList();

			return Ok(_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	[HttpGet("user/{userId}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> GetPetsByUserId(string userId)
	{
		try
		{
			if (!IsUserAuthorized(userId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["You are not authorized to view this resource"];
				return StatusCode(StatusCodes.Status403Forbidden, _response);
			}

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
			_response.Data = pets.ToPetDtoList();

			return Ok(_response);
		}
		catch (Exception e)
		{
			_response.IsSucceed = false;
			_response.ErrorMessages = [e.Message];
			return StatusCode(StatusCodes.Status500InternalServerError, _response);
		}
	}

	private bool IsUserAuthorized(string userId)
	{
		var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var loggedInUserRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

		return loggedInUserId == userId || loggedInUserRoles.Contains(UserRoles.Admin);
	}

	[HttpGet("{petId:int}", Name = "GetPetById")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
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

			if (!IsUserAuthorized(pet.OwnerId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["You are not authorized to view this resource"];
				return StatusCode(StatusCodes.Status403Forbidden, _response);
			}

			_response.IsSucceed = true;
			_response.Data = pet.ToPetDto();

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
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
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

			if (!IsUserAuthorized(petDto.OwnerId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["You are not authorized to create this resource"];
				return StatusCode(StatusCodes.Status403Forbidden, _response);
			}

			var pet = petDto.ToPet();
			pet.CreatedAt = DateTime.Now;
			pet.UpdatedAt = pet.CreatedAt;

			await petRepository.CreateAsync(pet);

			_response.IsSucceed = true;
			_response.Data = pet.ToPetDto();

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
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
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

			var pet = await petRepository.GetAsync(filter: p => p.Id == petId);

			if (!IsUserAuthorized(pet.OwnerId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["You are not authorized to delete this resource"];
				return StatusCode(StatusCodes.Status403Forbidden, _response);
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

	[HttpPut("{petId:int}")]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<ApiResponse>> UpdatePet(int petId, [FromBody] UpdatePetDto updatePetDto)
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

			if (petId != updatePetDto.Id)
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["petId mismatch"];
				return BadRequest(_response);
			}

			if (!await petRepository.ExistsAsync(filter: p => p.Id == petId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["Pet not found"];
				return NotFound(_response);
			}

			var petToUpdate = updatePetDto.ToPet();
			var ownerId = (await petRepository.GetAsync(filter: p => p.Id == petId)).OwnerId;

			if (!IsUserAuthorized(ownerId))
			{
				_response.IsSucceed = false;
				_response.ErrorMessages = ["You are not authorized to update this resource"];
				return StatusCode(StatusCodes.Status403Forbidden, _response);
			}
			
			var pet = await petRepository.UpdateAsync(petToUpdate);

			_response.IsSucceed = true;
			_response.Data = pet.ToPetDto();

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