using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetCareSystem.Models;
using PetCareSystem.Repositories.Contracts;
using PetCareSystem.Repositories.Implementations;
using PetCareSystem.StaticDetails;
using System.Security.Claims;

namespace PetCareSystem.CustomFilters;

public class ResourceAuthorizationFilter<T>(
	IRepository<T> repository, 
	UserManager<AppUser> userManager)
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

		await CheckQueryParams(context, userId);

		await CheckRouteParams(context, userId);
	}

	private async Task CheckRouteParams(AuthorizationFilterContext context, string? userId)
	{
		var routeValues = context.RouteData.Values;

		if (routeValues.TryGetValue("recordId", out var recordIdValue))
		{
			var recordId = int.Parse(recordIdValue.ToString());
			var record = await repository.GetAsync(filter: r => (r as MedicalRecord)!.Id == recordId, includeProperties: "Pet");

			if (record == null)
			{
				return; // return without raising an error, the controller will handle the 404 response
			}
			if ((record as MedicalRecord)!.Pet.OwnerId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
		else if (routeValues.TryGetValue("petId", out var petIdValue))
		{
			var petId = int.Parse(petIdValue.ToString());
			var pet = await repository.GetAsync(filter: p => (p as Pet)!.Id == petId);

			if (pet == null)
			{
				return; // return without raising an error, the controller will handle the 404 response
			}
			if ((pet as Pet)!.OwnerId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
		else if (routeValues.TryGetValue("userId", out var userIdValue))
		{
			var routeUserId = userIdValue.ToString();

			if (routeUserId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
	}

	private async Task CheckQueryParams(AuthorizationFilterContext context, string? userId)
	{
		var query = context.HttpContext.Request.Query;

		if (query.TryGetValue("petId", out var petIdValue))
		{
			var petId = int.Parse(petIdValue.ToString());
			var pet = await repository.GetAsync(p => (p as Pet)!.Id == petId);

			if (pet == null)
			{
				return; // return without raising an error, the controller will handle the 404 response
			}
			if ((pet as Pet)!.OwnerId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
		else if (query.TryGetValue("userId", out var userIdValue))
		{
			var routeUserId = userIdValue.ToString();

			if (routeUserId != userId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}