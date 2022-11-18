using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagement.Application.Queries.User.Login;

public class LoginUserQuery : IRequest<ModelStateDictionary>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public ModelStateDictionary State { get; set; }
}