using Microsoft.AspNetCore.Mvc;

namespace PetCareSystem.Controllers;

[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
	[HttpGet]
	public ActionResult<string> Get()
	{
		return Ok("Welcome to PetCareSystemApi!\nRefer to the API documentation in the link below:\nhttps://husteduvn-my.sharepoint.com/:w:/g/personal/vu_hn215171_sis_hust_edu_vn/EYCmMyiqRhFHlMZuYq6MYhkB5j5bvZZBFCkzTtzhda1u6w?e=sVIJGy");
	}
}