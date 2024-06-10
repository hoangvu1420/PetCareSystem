using Microsoft.AspNetCore.Mvc;

namespace PetCareSystem.CustomFilters;

public class ResourceAuthorizeAttribute(Type resourceType)
	: TypeFilterAttribute(typeof(ResourceAuthorizationFilter<>).MakeGenericType(resourceType));