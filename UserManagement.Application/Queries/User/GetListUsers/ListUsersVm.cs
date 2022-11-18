namespace UserManagement.Application.Queries.User.GetListUsers;

public class ListUsersVm 
{
    public IEnumerable<Domain.User> Users { get; set; }
}