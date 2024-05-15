namespace PetCareSystem.Models;

public class PetRoom
{
	public int PetId { get; set; }
	public Pet Pet { get; set; }

	public int RoomId { get; set; }
	public Room Room { get; set; }

	public DateTime CheckIn { get; set; }
	public DateTime CheckOut { get; set; }
	public bool IsIn { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Notes { get; set; }
}