using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.GrommingDtos;

public class UpdateGroomingServiceDto
{
	[Required]
	public int Id { get; set; }
	[Required]
	[MinLength(3)]
	public string Name { get; set; }
	[Required]
	[MinLength(3)]
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
}