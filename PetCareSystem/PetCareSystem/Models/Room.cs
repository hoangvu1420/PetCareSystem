namespace PetCareSystem.Models;

public class Room : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsIn { get; set; }

	public IEnumerable<PetRoom> PetRooms { get; set; }
}