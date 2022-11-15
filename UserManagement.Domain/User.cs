using Microsoft.AspNetCore.Identity;

namespace UserManagement.Domain;

public class User : IdentityUser<long>
{
    public DateTime LastLoginTime { get; set; }
    public DateTime RegistrationTime { get; set; }
    public string Status { get; set; }
}