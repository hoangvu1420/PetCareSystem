﻿using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.DTOs.AuthDtos;

public class LoginRequestDto
{
	[Required(ErrorMessage = "Email is required")]
	public string Email { get; init; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; init; }
}