using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;

namespace UserManagement.Application.Interfaces;

public interface IUserManagementDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}