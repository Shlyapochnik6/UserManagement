using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.User.Constants;

namespace UserManagement.Application.Commands.User.Blocking;

public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Unit>
{
    private readonly IUserManagementDbContext _context;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public BlockUserCommandHandler(IUserManagementDbContext context,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(u => request.SelectedUsers.Contains(u.Id));
        foreach (var u in users)
        {
            u.Status = AccountStatus.Blocked;

            if (u.Id == request.CurrentUserId)
                await _signInManager.SignOutAsync();
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}