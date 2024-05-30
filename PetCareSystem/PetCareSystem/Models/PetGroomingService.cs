using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareSystem.Models;

public class PetGroomingService : BaseEntity
{
	[ForeignKey("Pet")]
	public int PetId { get; set; }
	public Pet Pet { get; set; }

	[ForeignKey("GroomingService")]
	public int GroomingServiceId { get; set; }
	public GroomingService GroomingService { get; set; }

	public DateTime Date { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Notes { get; set; }
}