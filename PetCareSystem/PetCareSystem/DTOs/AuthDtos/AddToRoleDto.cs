using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.AuthDtos;

public class AddToRoleDto
{
	[Required(ErrorMessage = "Email is required")]
	public string Email { get; init; }

	[Required(ErrorMessage = "Role is required")]
	public string Role { get; init; }
}