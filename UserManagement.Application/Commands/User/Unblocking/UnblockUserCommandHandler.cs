using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.User.Constants;

namespace UserManagement.Application.Commands.User.Unblocking;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, Unit>
{
    private readonly IUserManagementDbContext _dbContext;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public UnblockUserCommandHandler(IUserManagementDbContext dbContext,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
    {
        var users = _dbContext.Users
            .Where(user => request.SelectedUsers.Contains(user.Id));
        foreach (var user in users)
            user.Status = AccountStatus.Active;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}