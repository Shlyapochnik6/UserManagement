using System.ComponentModel.DataAnnotations;
using AutoMapper;
using UserManagement.Application.Queries.User.Login;

namespace UserManagement.MVC.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Is not an e-mail")]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginViewModel, LoginUserQuery>()
            .ForMember(d => d.Email,
                opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Password,
                opt => opt.MapFrom(s => s.Password));
    }
}