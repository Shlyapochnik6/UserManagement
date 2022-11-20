using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Queries.User.GetUserQuery;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Domain.User?>
{
    private readonly IUserManagementDbContext _dbContext;

    public GetUserQueryHandler(IUserManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        return user;
    }
}