namespace PetCareSystem.DTOs.AuthDtos;

public class LoginResponse
{
	public bool IsSucceed { get; init; }
	public List<string> ErrorMessages { get; init; } = [];
	public string Token { get; init; }
	public UserDto UserInfo { get; init; }

}