using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Queries.User.GetListUsers;

public class GetListUsersQueryHandler : IRequestHandler<GetListUsersQuery, ListUsersVm>
{
    private readonly IUserManagementDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task<ListUsersVm> Handle(GetListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.ToListAsync();

        return new ListUsersVm() { Users = users };
    }
}