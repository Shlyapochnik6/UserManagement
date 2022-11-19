using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Commands.User.Removing;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserManagementDbContext _dbContext;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public RemoveUserCommandHandler(IUserManagementDbContext dbContext,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var users = _dbContext.Users.Where(u => request.SelectedUsers.Contains(u.Id));
        _dbContext.Users.RemoveRange(users);
        if (users.Contains(new Domain.User() { Id = request.CurrentUserId.Value }))
        {
            await _signInManager.SignOutAsync();
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}