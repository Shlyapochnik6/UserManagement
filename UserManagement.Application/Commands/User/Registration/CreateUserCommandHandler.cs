using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Application.Commands.User.Registration;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public CreateUserCommandHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email) == null;
        if (!userExists)
        {
            return false;
        }
        var user = _mapper.Map<Domain.User>(request);
        user.RegistrationTime = DateTime.Now;
        user.LastLoginTime = DateTime.Now;
        user.Status = string.Empty;
        user.UserName = Guid.NewGuid().ToString();
        await _userManager.CreateAsync(user, request.Password);
        await _signInManager.SignInAsync(user, false);
        return true;
    }
}