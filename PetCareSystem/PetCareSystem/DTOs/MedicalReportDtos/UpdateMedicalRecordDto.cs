using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.MedicalReportDtos;

public class UpdateMedicalReportDto
{
	[Required]
	public int Id { get; set; }
	[Required]
	public int PetId { get; set; }
	[Required]
	public string Diagnosis { get; set; }
	[Required]
	public string Doctor { get; set; }
	[Required]
	public string Diet { get; set; }
	[Required]
	public string Medication { get; set; }
	public string? Notes { get; set; }
	[Required]
	public DateTime NextAppointment { get; set; }
}