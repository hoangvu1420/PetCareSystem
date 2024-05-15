using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.DTOs.AuthDtos;
using PetCareSystem.Models;
using PetCareSystem.Services.Contracts;
using PetCareSystem.StaticDetails;

namespace PetCareSystem.Services.Implementations;

public class AuthService : IAuthService
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}

	public async Task<AuthResponse> RegisterAsync(RegisterRequestDto request)
	{
		var isUserExist = await _userManager.FindByEmailAsync(request.Email);
		if (isUserExist != null)
		{
			return new AuthResponse
			{
				IsSucceed = false,
				ErrorMessages = ["Email already exists"]
			};
		}

		var newUser = new AppUser
		{
			UserName = request.Email,
			Email = request.Email,
			FirstName = request.FirstName,
			LastName = request.LastName,
			District = request.Districs,
			ProfilePictureUrl = PictureStock.GetRandomPicture()
		};

		var result = await _userManager.CreateAsync(newUser, request.Password);

		if (!result.Succeeded)
		{
			return new AuthResponse
			{
				IsSucceed = false,
				ErrorMessages = result.Errors.Select(e => e.Description).ToList()
			};
		}

		if (await _userManager.Users.CountAsync() == 1)
		{
			await SeedRoles();
			await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
		}

		await _userManager.AddToRoleAsync(newUser, UserRoles.User);

		return new AuthResponse
		{
			IsSucceed = true,
			Message = "User created successfully"
		};
	}

	public async Task<LoginResponse> LoginAsync(LoginRequestDto request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user == null)
		{
			return new LoginResponse
			{
				IsSucceed = false,
				ErrorMessages = ["Email is not registered"]
			};
		}

		var result = await _userManager.CheckPasswordAsync(user, request.Password);

		if (!result)
		{
			return new LoginResponse
			{
				IsSucceed = false,
				ErrorMessages = ["Password is incorrect"]
			};
		}

		var userRoles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, user.Id),
			new(ClaimTypes.Email, user.Email),
			new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
			new("JWT_ID", Guid.NewGuid().ToString())
		};
		claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

		var token = GenerateJwtToken(claims);

		return new LoginResponse
		{
			IsSucceed = true,
			Token = token,
			UserInfo = new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				District = user.District,
				ProfilePictureUrl = user.ProfilePictureUrl,
				Roles = userRoles.ToList()
			}
		};
	}

	public async Task<AuthResponse> AddToRoleAsync(AddToRoleDto request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user == null)
		{
			return new AuthResponse
			{
				IsSucceed = false,
				ErrorMessages = ["Email is not registered"]
			};
		}

		var role = await _roleManager.FindByNameAsync(request.Role);
		if (role == null)
		{
			return new AuthResponse
			{
				IsSucceed = false,
				ErrorMessages = ["Role does not exist"]
			};
		}

		var result = await _userManager.AddToRoleAsync(user, request.Role);

		if (!result.Succeeded)
		{
			return new AuthResponse
			{
				IsSucceed = false,
				ErrorMessages = result.Errors.Select(e => e.Description).ToList()
			};
		}

		return new AuthResponse
		{
			IsSucceed = true,
			Message = $"User is added to {request.Role} role successfully"
		};
	}

	private string GenerateJwtToken(IEnumerable<Claim> claims)
	{
		var jwtTokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

		var tokenObject = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			expires: DateTime.Now.AddHours(24),
			claims: claims,
			signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		);

		var token = jwtTokenHandler.WriteToken(tokenObject);
		return token;
	}

	private async Task SeedRoles()
	{
		var roles = new List<string>
		{
			UserRoles.Admin, UserRoles.User
		};
		foreach (var role in roles)
		{
			if (await _roleManager.RoleExistsAsync(role)) continue;
			await _roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}