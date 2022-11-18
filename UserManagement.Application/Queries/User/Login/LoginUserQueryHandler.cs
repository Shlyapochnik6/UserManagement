using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserManagement.Application.Common.Constants;

namespace UserManagement.Application.Queries.User.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ModelStateDictionary>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public LoginUserQueryHandler(UserManager<Domain.User> userManager,
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<ModelStateDictionary> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            request.State.AddModelError("NonexistentUser", "The entered email doesn't exist");
            return request.State;
        }
        var correctPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!correctPassword)
        {
            request.State.AddModelError("IncorrectPassword", "The entered password is wrong");
            return request.State;
        }
        if (user.Status == AccountStatus.Blocked)
        {
            request.State.AddModelError("LockedUser", "The entered user is blocked");
            return request.State;
        }
        await _signInManager.SignInAsync(user, true);
        return request.State;
    }
}