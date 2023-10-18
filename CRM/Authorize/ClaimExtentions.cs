using System.Security.Claims;

namespace MyCRM.Authorize;

public static class ClaimExtentions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }
}