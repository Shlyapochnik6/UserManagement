using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Queries.User.GetListUsers;

public class GetListUsersQueryHandler : IRequestHandler<GetListUsersQuery, UsersVm>
{
    private readonly IUserManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListUsersQueryHandler(IUserManagementDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UsersVm> Handle(GetListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.ToListAsync(cancellationToken);

        return new UsersVm() { Users = users };
    }
}