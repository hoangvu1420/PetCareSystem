namespace PetCareSystem.StaticDetails;

public static class ProfilePictureStock
{
	public const string Dog1 =
		"https://images.unsplash.com/photo-1518020382113-a7e8fc38eac9?q=80&w=1617&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

	public const string Dog2 =
		"https://images.unsplash.com/photo-1558788353-f76d92427f16?q=80&w=1638&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

	public const string Cat1 =
		"https://images.unsplash.com/photo-1584197176155-304c9a3c03ce?q=80&w=1587&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

	public const string Cat2 =
		"https://images.unsplash.com/photo-1582725461742-8ecd962c260d?q=80&w=1587&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

	public static string GetRandomDog() => new Random().Next(0, 2) switch
	{
		0 => Dog1,
		1 => Dog2,
		_ => Dog1
	};

	public static string GetRandomCat() => new Random().Next(0, 2) switch
	{
		0 => Cat1,
		1 => Cat2,
		_ => Cat1
	};

	public static string GetRandomProfilePicture() => new Random().Next(0, 2) switch
	{
		0 => GetRandomDog(),
		1 => GetRandomCat(),
		_ => GetRandomDog()
	};
}
