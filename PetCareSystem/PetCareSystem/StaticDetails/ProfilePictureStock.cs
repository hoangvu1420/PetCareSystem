namespace PetCareSystem.StaticDetails;

public static class ProfilePictureStock
{
	public static readonly List<string> DogPictures =
	[
		"https://images.unsplash.com/photo-1518020382113-a7e8fc38eac9?q=80&w=1617&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
		"https://images.unsplash.com/photo-1558788353-f76d92427f16?q=80&w=1638&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
	];

	public static readonly List<string> CatPictures =
	[
		"https://images.unsplash.com/photo-1584197176155-304c9a3c03ce?q=80&w=1587&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
		"https://images.unsplash.com/photo-1582725461742-8ecd962c260d?q=80&w=1587&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
	];

	public static string GetRandomDogPictures()
	{
		var random = new Random().Next(0, 2);
		return DogPictures[random];
	}

	public static string GetRandomCatPictures()
	{
		var random = new Random().Next(0, 2);
		return CatPictures[random];
	}

	public static string GetRandomProfilePicture()
	{
		var random = new Random().Next(0, 1);
		return random == 0 ? GetRandomDogPictures() : GetRandomCatPictures();
	}
}