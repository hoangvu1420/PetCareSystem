using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.RoomDtos;

public class CreateRoomDto
{
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
}