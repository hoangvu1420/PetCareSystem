namespace PetCareSystem.DTOs.RoomDtos;

public class RoomDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public double Price { get; set; }
	public string Description { get; set; }
	public int BookedCount { get; set; }
}