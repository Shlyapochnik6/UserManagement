using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Common.Exceptions;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Commands.User.Registration;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public CreateUserCommandHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //Add registration time
    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email) == null;
        if (!userExists)
        {
            throw new UserExistsException();
        }
        
        var user = _mapper.Map<Domain.User>(request);
        user.RegistrationTime = DateTime.Now;
        user.LastLoginTime = DateTime.Now;
        user.Status = null;
        await _userManager.AddPasswordAsync(user, request.Password);
        
        await _userManager.CreateAsync(user);
        await _signInManager.SignInAsync(user, false);
        
        return new IdentityResult().Succeeded;
    }
}