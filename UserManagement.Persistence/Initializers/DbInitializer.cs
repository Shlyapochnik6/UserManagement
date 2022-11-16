using UserManagement.Persistence.Contexts;

namespace UserManagement.Persistence.Initializers;

public class DbInitializer
{
    public static void Initialize(UserManagementDbContext context)
    {
        context.Database.EnsureCreated();
    }
}