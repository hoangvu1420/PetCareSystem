﻿using Microsoft.AspNetCore.Identity;

namespace PetCareSystem.Models;

public class AppUser : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string? Districs { get; set; }
	public string ProfilePictureUrl { get; set; }
	public string FullName => $"{FirstName} {LastName}";
}