namespace PetCareSystem.DTOs.GroomingServiceBookingDtos;

public class CreateGroomingServiceBookingDto
{
	public int PetId { get; set; }
	public int GroomingServiceId { get; set; }
	public DateTime BookingDate { get; set; }
	public string? Notes { get; set; }
}