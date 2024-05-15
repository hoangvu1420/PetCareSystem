namespace PetCareSystem.Models;

public class Pet : BaseEntity
{
	public string Name { get; set; }
	public int Age { get; set; }
	public string Breed { get; set; }
	public string HairColor { get; set; }

	// Foreign Key for AppUser
	public int OwnerId { get; set; }
	public AppUser Owner { get; set; }

	// Navigation Properties
	public IEnumerable<PetGroomingService> PetGroomingServices { get; set; }
	public IEnumerable<PetRoom> PetRooms { get; set; }
}