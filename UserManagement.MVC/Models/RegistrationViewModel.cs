using System.ComponentModel.DataAnnotations;

namespace UserManagement.MVC.Models;

public class RegistrationViewModel
{
    [Required] 
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Is not an e-mail")]
    public string Email { get; set; }

    [Required] 
    [MinLength(1)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Password don't match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; set; }
}