namespace UserManagement.Application.Queries.User.GetListUsers;

public class UsersVm 
{
    public IEnumerable<Domain.User> Users { get; set; }
}