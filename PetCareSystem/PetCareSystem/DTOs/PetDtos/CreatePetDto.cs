using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.PetDtos;

public class CreatePetDto
{
	[Required]
	public string Name { get; set; }
	[Required]
	public int Age { get; set; }
	[Required]
	public string Gender { get; set; }
	[Required]
	public string HairColor { get; set; }
	[Required]
	public string Species { get; set; }
	[Required]
	public string Breed { get; set; }
	public string? ImageUrl { get; set; }
	[Required]
	public string OwnerId { get; set; }
}