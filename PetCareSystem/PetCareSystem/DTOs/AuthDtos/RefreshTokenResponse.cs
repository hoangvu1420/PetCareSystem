namespace PetCareSystem.DTOs.AuthDtos;

public class RefreshTokenResponse
{
	public bool IsSucceed { get; init; }
	public List<string> ErrorMessages { get; init; } = [];
	public string Token { get; init; }
	public DateTime ExpirationDate { get; init; }
	public string RefreshToken { get; init; }
}