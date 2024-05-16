namespace PetCareSystem.DTOs.PetDtos;

public class PetDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int Age { get; set; }
	public string Gender { get; set; }
	public string HairColor { get; set; }
	public string Species { get; set; }
	public string Breed { get; set; }
	public string ImageUrl { get; set; }
	public string OwnerId { get; set; }
}