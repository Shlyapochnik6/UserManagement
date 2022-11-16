using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain;
using UserManagement.Persistence.EntityTypeConfigurations;

namespace UserManagement.Persistence.Contexts;

public class UserManagementDbContext : IdentityDbContext<User, IdentityRole<long>, long>, IUserManagementDbContext
{
    public DbSet<User> Users { get; set; }

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("User");
        builder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(builder);
    }
}