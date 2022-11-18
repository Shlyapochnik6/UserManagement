using AutoMapper;
using MediatR;
using UserManagement.Application.Common.Mappings;

namespace UserManagement.Application.Commands.User.Registration;

public class CreateUserCommand : IRequest<bool>, IMapWith<Domain.User>
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, Domain.User>()
            .ForMember(d => d.Email,
                opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Name,
                opt => opt.MapFrom(s => s.Name));
    }
}