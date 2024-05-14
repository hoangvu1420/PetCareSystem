using Microsoft.AspNetCore.Identity;

namespace PetCareSystem.Models;

public class AppUser : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Address { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
	public string ProfilePictureUrl { get; set; }
	public string FullName => $"{FirstName} {LastName}";
}