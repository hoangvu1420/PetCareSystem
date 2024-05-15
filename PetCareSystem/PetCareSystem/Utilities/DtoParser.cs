using PetCareSystem.DTOs.PetDtos;
using PetCareSystem.Models;
using PetCareSystem.StaticDetails;

namespace PetCareSystem.Utilities;

public static class DtoParser
{
	public static Pet ToPet(this CreatePetDto createPetDto)
	{
		string imageUrl;
		if (createPetDto.ImageUrl == null)
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
}