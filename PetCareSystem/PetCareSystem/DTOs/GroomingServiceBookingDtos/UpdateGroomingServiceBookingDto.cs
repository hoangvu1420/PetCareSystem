namespace PetCareSystem.DTOs.GroomingServiceBookingDtos;

public class UpdateGroomingServiceBookingDto
{
	public int Id { get; set; }
	public DateTime BookingDate { get; set; }
	public string? Notes { get; set; }
}