namespace PetCareSystem.DTOs;

public class ApiResponse
{
	public bool IsSucceed { get; set; } = true;
	public List<string> ErrorMessages { get; set; } = [];
	public object Data { get; set; }
}