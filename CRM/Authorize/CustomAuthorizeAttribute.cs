using Microsoft.AspNetCore.Mvc.Filters;
using MyCRM.Database;

namespace MyCRM.Authorize;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : Attribute,IAuthorizationFilter
{
    private MainDbContext _dbcontext;

   // private readonly IList<Role> _roles;



    public string Roles;
   

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _dbcontext = context.HttpContext.RequestServices.GetRequiredService<MainDbContext>();

        var user = ClaimExtentions.GetUsername(context.HttpContext.User);
        
    }
}