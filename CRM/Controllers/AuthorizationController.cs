using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyCRM.Authorize;
using MyCRM.Authorize.Responses;
using MyCRM.Database;
using MyCRM.Model;
using MyCRM.Responses;

namespace MyCRM.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    public readonly JwtOptions _options;
    public readonly MainDbContext _dbContext;

    public AuthorizationController(IOptions<JwtOptions> options,MainDbContext dbcontext)
    {
        _options = options.Value;
        _dbContext = dbcontext;
    }
    
    
    [HttpGet("Login")]
    public async Task<ActionResult<LoginResponse>> GetToken(int UserId,string password)
   {
       var user = await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(i => i.Id == UserId && i.Password == password);
       if (user == null)
           return NotFound();
        
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name,user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, user.Role.Role.DisplayName()));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddYears(10),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        var response = new LoginResponse(new JwtSecurityTokenHandler().WriteToken(jwt), user);
        return new ObjectResult(response);

    }


   [HttpGet]
   [CustomAuthorize(Roles = "admin")]
   public async Task<string> test()
   {
       return "success!!";
   }
}