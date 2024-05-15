namespace PetCareSystem.Models;

public class MedicalRecord : BaseEntity
{
	public DateTime Date { get; set; }
	public string Diagnosis { get; set; }
	public DateTime NextAppointment { get; set; }
	public string Doctor { get; set; }
	public string Diet { get; set; }
	public string Medication { get; set; }

	public int PetId { get; set; }
	public Pet Pet { get; set; }
}