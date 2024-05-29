namespace PetCareSystem.DTOs.AuthDtos;

public class LoginResponse
{
	public bool IsSucceed { get; init; }
	public List<string> ErrorMessages { get; init; } = [];
	public string Token { get; init; }
	public DateTime ExpirationDate { get; init; }
	public string RefreshToken { get; init; }
	public UserDto UserInfo { get; init; }

}