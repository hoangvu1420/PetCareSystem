namespace PetCareSystem.DTOs.RoomBookingDtos;

public class UpdateRoomBookingDto
{
	public int Id { get; set; }

	public DateTime CheckIn { get; set; }
	public DateTime CheckOut { get; set; }

	public string? Notes { get; set; }
}