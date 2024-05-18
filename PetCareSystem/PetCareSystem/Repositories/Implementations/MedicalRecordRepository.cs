using PetCareSystem.Infrastructure.DataContext;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;

namespace PetCareSystem.Repositories.Implementations;

public class MedicalRecordRepository(ApplicationDbContext dbContext)
	: Repository<MedicalRecord>(dbContext), IMedicalRecordRepository;