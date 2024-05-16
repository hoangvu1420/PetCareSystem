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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<PetGroomingService>()
			.HasKey(pgs => new { pgs.PetId, pgs.GroomingServiceId });

		modelBuilder.Entity<PetRoom>()
			.HasKey(pr => new { pr.PetId, pr.RoomId });
	}
}