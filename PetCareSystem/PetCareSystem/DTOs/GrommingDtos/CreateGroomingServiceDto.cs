using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.GrommingDtos;

public class CreateGroomingServiceDto
{
	[Required]
	[MinLength(3)]
	public string Name { get; set; }
	[Required]
	[MinLength(10)]
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
}
