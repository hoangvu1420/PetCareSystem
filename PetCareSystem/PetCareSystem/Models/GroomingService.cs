namespace PetCareSystem.Models;

public class GroomingService : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }

	public IEnumerable<PetGroomingService> PetGroomingServices { get; set; }
}