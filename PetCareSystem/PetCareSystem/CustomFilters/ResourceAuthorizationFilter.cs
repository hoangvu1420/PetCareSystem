using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.StaticDetails;
using System.Security.Claims;

namespace PetCareSystem.CustomFilters;

public class ResourceAuthorizationFilter<T>(IRepository<T> repository, UserManager<AppUser> userManager)
	: IAsyncAuthorizationFilter
	where T : class
{
	private readonly UserManager<AppUser> _userManager = userManager;

	public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
	{
		var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

		if (userRoles.Contains(UserRoles.Admin))
		{
			return; // Admins can access any resource
		}

		var routeData = context.RouteData.Values;
		if (routeData.TryGetValue("petId", out var petIdValue))
		{
			var petId = int.Parse(petIdValue.ToString());
			var pet = await repository.GetAsync(p => (p as Pet)!.Id == petId);

			if (pet == null || (pet as Pet)!.OwnerId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
		else if (routeData.TryGetValue("userId", out var userIdValue))
		{
			var routeUserId = userIdValue.ToString();

			if (routeUserId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}