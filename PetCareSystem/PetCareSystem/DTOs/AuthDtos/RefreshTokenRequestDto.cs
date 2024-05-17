using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.AuthDtos;

public class RefreshTokenRequestDto
{
	[Required]
	public string UserId { get; set; }
	[Required]
	public string RefreshToken { get; set; }
}