namespace PetCareSystem.DTOs.MedicalReportDtos;

public class MedicalRecordDto
{
	public int Id { get; set; }
	public int PetId { get; set; }
	public DateTime Date { get; set; }
	public string Diagnosis { get; set; }
	public string Doctor { get; set; }
	public string Diet { get; set; }
	public string Medication { get; set; }
	public string? Notes { get; set; }
	public DateTime NextAppointment { get; set; } 
}