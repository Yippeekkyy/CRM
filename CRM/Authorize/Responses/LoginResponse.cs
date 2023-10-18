using MyCRM.Model;

namespace MyCRM.Authorize.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public User User { get; set; }

    public LoginResponse(string token, User user)
    {
        Token = token;
        User = user;
    }
}