using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Models;

namespace PetCareSystem.Infrastructure.DataContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
	DbSet<AppUser> AppUsers { get; set; }
	DbSet<Pet> Pets { get; set; }
	DbSet<MedicalRecord> MedicalRecords { get; set; }
	DbSet<GroomingService> GroomingServices { get; set; }
	DbSet<PetGroomingService> PetGroomingServices { get; set; }
	DbSet<Room> Rooms { get; set; }
	DbSet<PetRoom> PetRooms { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<PetGroomingService>()
			.HasKey(pgs => new { pgs.PetId, pgs.GroomingServiceId });

		modelBuilder.Entity<PetRoom>()
			.HasKey(pr => new { pr.PetId, pr.RoomId });
	}
}