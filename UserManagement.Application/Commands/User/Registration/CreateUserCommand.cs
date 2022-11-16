using AutoMapper;
using MediatR;
using UserManagement.Application.Common.Mappings;

namespace UserManagement.Application.Commands.User.Registration;

public class CreateUserCommand : IRequest<bool>, IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        
    }
}