namespace PetCareSystem.Models;

public class PetGroomingService
{
	public int PetId { get; set; }
	public Pet Pet { get; set; }

	public int GroomingServiceId { get; set; }
	public GroomingService GroomingService { get; set; }

	public DateTime Date { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Notes { get; set; }
}