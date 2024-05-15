using Microsoft.IdentityModel.Tokens;
using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.Models;
using PetCareSystem.StaticDetails;

namespace PetCareSystem.Utilities;

public static class DtoParser
{
	public static Pet ToPet(this CreatePetDto createPetDto)
	{
		string imageUrl;
		if (createPetDto.ImageUrl.IsNullOrEmpty())
		{
			imageUrl = createPetDto.Species switch
			{
				"Dog" => PictureStock.GetRandomDogPicture(),
				"Cat" => PictureStock.GetRandomCatPicture(),
				_ => PictureStock.GetRandomPicture()
			};
		}
		else
		{
			imageUrl = createPetDto.ImageUrl;
		}

		return new Pet
		{
			Name = createPetDto.Name,
			Age = createPetDto.Age,
			HairColor = createPetDto.HairColor,
			Species = createPetDto.Species,
			Breed = createPetDto.Breed,
			ImageUrl = imageUrl,
			OwnerId = createPetDto.OwnerId
		};
	}

	public static PetDto ToPetDto(this Pet pet)
	{
		return new PetDto
		{
			Id = pet.Id,
			Name = pet.Name,
			Age = pet.Age,
			HairColor = pet.HairColor,
			Species = pet.Species,
			Breed = pet.Breed,
			ImageUrl = pet.ImageUrl,
			OwnerId = pet.OwnerId
		};
	}

	public static IEnumerable<PetDto> ToPetDtoList(this IEnumerable<Pet> pets)
	{
		return pets.Select(pet => pet.ToPetDto()).ToList();
	}
}