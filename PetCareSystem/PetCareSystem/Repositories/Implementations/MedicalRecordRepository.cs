using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class MedicalRecordRepository(ApplicationDbContext dbContext)
	: Repository<MedicalRecord>(dbContext), IMedicalRecordRepository
{
	private readonly ApplicationDbContext _dbContext1 = dbContext;

	public new async Task CreateAsync(MedicalRecord medicalRecord)
	{
		medicalRecord.Date = DateTime.Now;
		medicalRecord.CreatedAt = medicalRecord.Date;
		medicalRecord.UpdatedAt = medicalRecord.Date;

		await _dbContext1.MedicalRecords.AddAsync(medicalRecord);
		await _dbContext1.SaveChangesAsync();
	}

	public async Task<MedicalRecord?> UpdateAsync(MedicalRecord medicalRecord)
	{
		var medicalRecordToUpdate = await _dbContext1.MedicalRecords.FindAsync(medicalRecord.Id);
		if (medicalRecordToUpdate == null)
			return medicalRecordToUpdate;

		medicalRecordToUpdate.Diagnosis = medicalRecord.Diagnosis;
		medicalRecordToUpdate.NextAppointment = medicalRecord.NextAppointment;
		medicalRecordToUpdate.Doctor = medicalRecord.Doctor;
		medicalRecordToUpdate.Diet = medicalRecord.Diet;
		medicalRecordToUpdate.Medication = medicalRecord.Medication;
		medicalRecordToUpdate.Notes = medicalRecord.Notes;
		medicalRecordToUpdate.UpdatedAt = DateTime.Now;

		await _dbContext1.SaveChangesAsync();

		return medicalRecordToUpdate;
	}
}
