namespace PetCareSystem.DTOs.RoomBookingDtos;

public class CreateRoomBookingDto
{
	public int PetId { get; set; }
	public int RoomId { get; set; }

	public DateTime CheckIn { get; set; }
	public DateTime CheckOut { get; set; }

	public string? Notes { get; set; }
}