using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.DTOs.AuthDtos;
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
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

		var registerResult = await authService.RegisterAsync(registerDto);

		if (registerResult.IsSucceed)
			return Ok(registerResult);

		return BadRequest(registerResult);
	}

	[HttpPost]
	[Route("login")]
	[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
	{
		if (!ModelState.IsValid)
		{
			var errorMessages = ModelState.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.ToList();

			return BadRequest(new LoginResponse
			{
				IsSucceed = false,
				ErrorMessages = errorMessages
			});
		}

		var loginResult = await authService.LoginAsync(loginDto);

		if (loginResult.IsSucceed)
			return Ok(loginResult);

		return BadRequest(loginResult);
	}

	[HttpPost]
	[Route("refresh-token")]
	[ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
	{
		if (!ModelState.IsValid)
		{
			var errorMessages = ModelState.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.ToList();

			return BadRequest(new RefreshTokenResponse
			{
				IsSucceed = false,
				ErrorMessages = errorMessages
			});
		}

		var refreshTokenResult = await authService.IssueRefreshTokenAsync(request);

		if (refreshTokenResult.IsSucceed)
			return Ok(refreshTokenResult);

		return BadRequest(refreshTokenResult);
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

		var addToRoleResult = await authService.AddToRoleAsync(addToRoleDto);

		if (addToRoleResult.IsSucceed)
			return Ok(addToRoleResult);

		return BadRequest(addToRoleResult);
	}
}