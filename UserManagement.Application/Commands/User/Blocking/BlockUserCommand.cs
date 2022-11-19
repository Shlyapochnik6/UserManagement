using MediatR;

namespace UserManagement.Application.Commands.User.Blocking;

public class BlockUserCommand : IRequest
{
    public long? CurrentUserId { get; set; }
    
    public IEnumerable<long> SelectedUsers { get; set; }
}