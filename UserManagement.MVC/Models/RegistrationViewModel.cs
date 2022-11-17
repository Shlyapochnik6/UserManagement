using System.ComponentModel.DataAnnotations;
using AutoMapper;
using UserManagement.Application.Commands.User.Registration;
using UserManagement.Application.Common.Mappings;

namespace UserManagement.MVC.Models;

public class RegistrationViewModel : IMapWith<CreateUserCommand>
{
    [Required] 
    public string Name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Is not an e-mail")]
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Password don't match")]
    public string PasswordConfirm { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegistrationViewModel, CreateUserCommand>()
            .ForMember(d => d.Name,
                opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Email,
                opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Password,
                opt => opt.MapFrom(s => s.Password));
    }
}