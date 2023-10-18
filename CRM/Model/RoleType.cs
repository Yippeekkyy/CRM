using System.ComponentModel.DataAnnotations;

namespace MyCRM.Model;

public enum RoleType
{
    [Display(Name = "Admin")] Admin = 0,
    [Display(Name = "Waiter")] Waiter
}