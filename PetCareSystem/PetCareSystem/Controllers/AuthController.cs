using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs;
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Controllers;

[Route("/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost]
	[Route("register")]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
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

		var registerResult = await _authService.RegisterAsync(registerDto);

		if (registerResult.IsSucceed)
			return Ok(registerResult);

		return BadRequest(registerResult);
	}

	[HttpPost]
	[Route("login")]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
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

		var loginResult = await _authService.LoginAsync(loginDto);

		if (loginResult.IsSucceed)
			return Ok(loginResult);

		return BadRequest(loginResult);
	}

	[HttpPost]
	[Route("add-to-role")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddToRole([FromBody] AddToRoleDto addToRoleDto)
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

		var addToRoleResult = await _authService.AddToRoleAsync(addToRoleDto);

		if (addToRoleResult.IsSucceed)
			return Ok(addToRoleResult);

		return BadRequest(addToRoleResult);
	}
}