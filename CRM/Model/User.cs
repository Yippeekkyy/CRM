using Microsoft.AspNetCore.Identity;

namespace MyCRM.Model;

public class User 
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int Phone { get; set; }
    
    public string? Password { get; set; } 
    
    public int RoleId { get; set; }
    public UserRole Role { get; set; }
}