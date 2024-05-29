using Microsoft.AspNetCore.Mvc;
using Moq;
using PetCareSystem.Controllers;
using PetCareSystem.DTOs.AuthDtos;
using PetCareSystem.Services.Contracts;

namespace PetCareSystem.Test.AuthTests;

public class AuthControllerTests
{
	private readonly AuthController _controller;
	private readonly Mock<IAuthService> _mockAuthService;

	public AuthControllerTests()
	{
		_mockAuthService = new Mock<IAuthService>();
		_controller = new AuthController(_mockAuthService.Object);
	}

	[Fact]
	public async Task Register_ValidRequest_ReturnsOk()
	{
		// Arrange
		var registerDto = new RegisterRequestDto
		{

		};
		var authResponse = new AuthResponse
		{
			IsSucceed = true
		};

		_mockAuthService.Setup(service => service.RegisterAsync(registerDto))
			.ReturnsAsync(authResponse);

		// Act
		var result = await _controller.Register(registerDto);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.IsType<AuthResponse>(okResult.Value);
	}

	[Fact]
	public async Task Register_InvalidModel_ReturnsBadRequest()
	{
		// Arrange
		_controller.ModelState.AddModelError("some error", "some more error");

		// Act
		var result = await _controller.Register(new RegisterRequestDto());

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.IsType<AuthResponse>(badRequestResult.Value);
	}

	[Fact]
	public async Task Login_ValidRequest_ReturnsOk()
	{
		// Arrange
		var loginDto = new LoginRequestDto { Email = "testemail", Password = "testpassword" };
		var loginResponse = new LoginResponse { IsSucceed = true };

		_mockAuthService.Setup(service => service.LoginAsync(loginDto))
			.ReturnsAsync(loginResponse);

		var controller = new AuthController(_mockAuthService.Object);

		// Act
		var result = await controller.Login(loginDto);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var returnValue = Assert.IsType<LoginResponse>(okResult.Value);
		Assert.Equal(loginResponse, returnValue);
	}
}