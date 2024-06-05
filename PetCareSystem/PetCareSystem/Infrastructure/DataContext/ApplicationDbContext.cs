using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Models;

namespace PetCareSystem.Infrastructure.DataContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<Pet> Pets { get; set; }
	public DbSet<MedicalRecord> MedicalRecords { get; set; }
	public DbSet<GroomingService> GroomingServices { get; set; }
	public DbSet<PetGroomingService> PetGroomingServices { get; set; }
	public DbSet<Room> Rooms { get; set; }
	public DbSet<PetRoom> PetRooms { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<MedicalRecord>()
			.HasOne(m => m.Pet)
			.WithMany(p => p.MedicalRecords)
			.HasForeignKey(m => m.PetId);
	}
}