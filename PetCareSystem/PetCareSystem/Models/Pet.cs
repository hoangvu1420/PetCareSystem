using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareSystem.Models;

public class Pet : BaseEntity
{
	public string Name { get; set; }
	public int Age { get; set; }
	public string Species { get; set; }
	public string Breed { get; set; }
	public string Gender { get; set; }
	public string HairColor { get; set; }
	public string ImageUrl { get; set; }

	[ForeignKey("AppUser")]
	public string OwnerId { get; set; }
	public AppUser Owner { get; set; }

	// Navigation Properties
	public IEnumerable<MedicalRecord> MedicalRecords { get; set; }
	public IEnumerable<PetGroomingService> PetGroomingServices { get; set; }
	public IEnumerable<PetRoom> PetRooms { get; set; }
}