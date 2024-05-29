using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.AuthDtos;

public class RegisterRequestDto
{
	[Required(ErrorMessage = "First name is required")]
	public string FirstName { get; init; }

	[Required(ErrorMessage = "Last name is required")]
	public string LastName { get; init; }

	public string? District { get; init; }

	public string? ProfilePictureUrl { get; init; }

	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address")]
	public string Email { get; init; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; init; }
}