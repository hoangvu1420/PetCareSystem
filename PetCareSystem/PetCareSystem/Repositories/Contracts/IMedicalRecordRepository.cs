using PetCareSystem.Models;

namespace PetCareSystem.Repositories.Contracts;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
	new Task CreateAsync(MedicalRecord medicalRecord);
	Task<MedicalRecord?> UpdateAsync(MedicalRecord medicalRecord);
}