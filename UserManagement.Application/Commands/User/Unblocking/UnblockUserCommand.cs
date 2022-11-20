using MediatR;

namespace UserManagement.Application.Commands.User.Unblocking;

public class UnblockUserCommand : IRequest
{
    public IEnumerable<long> SelectedUsers { get; set; }
}