using MediatR;

namespace UserManagement.Application.Queries.User.GetUserQuery;

public class GetUserQuery : IRequest<Domain.User?>
{
    public long? UserId { get; set; }
}