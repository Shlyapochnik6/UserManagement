using MediatR;

namespace UserManagement.Application.Commands.User.Removing;

public class RemoveUserCommand : IRequest
{
    public long? CurrentUserId { get; set; }
    public IEnumerable<long> SelectedUsers { get; set; }
}