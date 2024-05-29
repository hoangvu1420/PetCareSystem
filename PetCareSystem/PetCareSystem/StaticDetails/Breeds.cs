using Newtonsoft.Json;

namespace PetCareSystem.StaticDetails;

public static class Breeds
{
	public static string CatBreedsJson { get; set; }
	public static string DogBreedsJson { get; set; }

	public static List<Breed> CatBreeds => GetCatBreeds();
	public static List<Breed> DogBreeds => GetDogBreeds();

	public static List<Breed> GetCatBreeds()
	{
		List<Breed> catBreeds = JsonConvert.DeserializeObject<List<Breed>>(CatBreedsJson);
		return catBreeds;
	}

	public static List<Breed> GetDogBreeds()
	{
		List<Breed> dogBreeds = JsonConvert.DeserializeObject<List<Breed>>(DogBreedsJson);
		return dogBreeds;
	}

	public static Breed GetRandomCatBreed()
	{
		var random = new Random();
		var randomIndex = random.Next(0, CatBreeds.Count);
		return CatBreeds[randomIndex];
	}

	public static Breed GetRandomDogBreed()
	{
		var random = new Random();
		var randomIndex = random.Next(0, DogBreeds.Count);
		return DogBreeds[randomIndex];
	}

	public static Breed GetCatBreed(string breedName)
	{
		return CatBreeds.FirstOrDefault(b => b.BreedName.ToLower().Contains(breedName.ToLower()));
	}

	public static Breed GetDogBreed(string breedName)
	{
		return DogBreeds.FirstOrDefault(b => b.BreedName.ToLower().Contains(breedName.ToLower()));
	}

	public static string GetRandomCatImage => GetRandomCatBreed().ImageUrl;

	public static string GetRandomDogImage => GetRandomDogBreed().ImageUrl;

	public static string GetRandomImage => new Random().Next(0, 2) == 0 ? GetRandomCatImage : GetRandomDogImage;
}

public struct Breed
{
	public string BreedName { get; set; }
	public string ImageUrl { get; set; }
}