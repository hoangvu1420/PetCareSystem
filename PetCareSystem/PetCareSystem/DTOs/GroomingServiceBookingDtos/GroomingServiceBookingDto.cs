namespace PetCareSystem.DTOs.GroomingServiceBookingDtos;

public class GroomingServiceBookingDto
{
	public int Id { get; set; }
	public int PetId { get; set; }
	public string PetName { get; set; }
	public int GroomingServiceId { get; set; }
	public string GroomingServiceName { get; set; }
	public DateTime BookingDate { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Notes { get; set; }
}