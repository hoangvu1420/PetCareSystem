using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MockQueryable.Moq;
using Moq;
using PetCareSystem.DTOs.AuthDtos;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Services.Implementations;

// To use BuildMock

namespace PetCareSystem.Test.AuthTests;

public class AuthServiceTests
{
	private readonly Mock<UserManager<AppUser>> _mockUserManager;
	private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
	private readonly Mock<IConfiguration> _mockConfiguration;
	private readonly Mock<IRefreshTokenRepository> _mockRefreshTokenRepository;
	private readonly AuthService _authService;

	public AuthServiceTests()
	{
		_mockUserManager = MockUserManager<AppUser>();
		_mockRoleManager = MockRoleManager();
		_mockConfiguration = new Mock<IConfiguration>();
		_mockRefreshTokenRepository = new Mock<IRefreshTokenRepository>();

		_authService = new AuthService(
			_mockUserManager.Object,
			_mockRoleManager.Object,
			_mockConfiguration.Object,
			_mockRefreshTokenRepository.Object);
	}

	[Fact]
	public async Task RegisterAsync_UserAlreadyExists_ReturnsError()
	{
		// Arrange
		var request = new RegisterRequestDto
		{
			Email = "test@example.com", 
			Password = "Password123!"
		};
		_mockUserManager.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(new AppUser());

		// Act
		var result = await _authService.RegisterAsync(request);

		// Assert
		Assert.False(result.IsSucceed);
		Assert.Contains("Email already exists", result.ErrorMessages);
	}

	[Fact]
	public async Task RegisterAsync_SuccessfulRegistration_ReturnsSuccess()
	{
		// Arrange
		var request = new RegisterRequestDto
		{
			Email = "newuser@example.com", 
			Password = "Password123!"
		};
		_mockUserManager.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync((AppUser)null!);
		_mockUserManager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), request.Password))
			.ReturnsAsync(IdentityResult.Success);

		var users = new List<AppUser>().AsQueryable().BuildMock();
		_mockUserManager.Setup(um => um.Users).Returns(users);

		// Act
		var result = await _authService.RegisterAsync(request);

		// Assert
		Assert.True(result.IsSucceed);
		Assert.Equal("User created successfully", result.Message);
	}

	[Fact]
	public async Task RegisterAsync_InvalidEmailRequest_ReturnsError()
	{
		// Arrange
		var invalidRegisterDto = new RegisterRequestDto
		{
			FirstName = "first",
			LastName = "last",
			Email = "invalidemail", 
			Password = "Password123!"
		};

		_mockUserManager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), invalidRegisterDto.Password))
			.ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid email address" }));

		var authService = new AuthService(
			_mockUserManager.Object, 
			_mockRoleManager.Object, 
			_mockConfiguration.Object, 
			_mockRefreshTokenRepository.Object);

		// Act
		var result = await authService.RegisterAsync(invalidRegisterDto);

		// Assert
		Assert.False(result.IsSucceed);
		Assert.Contains("Invalid email address", result.ErrorMessages);
	}


	private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
	{
		var store = new Mock<IUserStore<TUser>>();
		var passwordValidator = new Mock<IPasswordValidator<TUser>>();
		var passwordHasher = new Mock<IPasswordHasher<TUser>>();
		var userValidators = new List<IUserValidator<TUser>> { new Mock<IUserValidator<TUser>>().Object };
		var pwdValidators = new List<IPasswordValidator<TUser>> { passwordValidator.Object };

		passwordValidator.Setup(v => v.ValidateAsync(It.IsAny<UserManager<TUser>>(), It.IsAny<TUser>(), It.IsAny<string>()))
			.ReturnsAsync(IdentityResult.Success);

		return new Mock<UserManager<TUser>>(store.Object, null!, passwordHasher.Object, userValidators, pwdValidators, null!, null!, null!, null!);
	}

	private Mock<RoleManager<IdentityRole>> MockRoleManager()
	{
		var store = new Mock<IRoleStore<IdentityRole>>();
		return new Mock<RoleManager<IdentityRole>>(store.Object, null!, null!, null!, null!);
	}
}