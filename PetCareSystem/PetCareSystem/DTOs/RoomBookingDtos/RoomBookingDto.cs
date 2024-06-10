namespace PetCareSystem.DTOs.RoomBookingDtos;

public class RoomBookingDto
{
	public int Id { get; set; }

	public int PetId { get; set; }
	public string PetName { get; set; }

	public int RoomId { get; set; }
	public string RoomName { get; set; }

	public DateTime CheckIn { get; set; }
	public DateTime CheckOut { get; set; }

	public int TotalDays { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Notes { get; set; }
}