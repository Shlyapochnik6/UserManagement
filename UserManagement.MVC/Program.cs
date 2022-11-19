using System.Reflection;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application;
using UserManagement.Application.Common.Mappings;
using UserManagement.Application.Interfaces;
using UserManagement.Domain;
using UserManagement.MVC.FilterAttributes;
using UserManagement.Persistence;
using UserManagement.Persistence.Contexts;
using UserManagement.Persistence.Initializers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IUserManagementDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<UserManagementDbContext>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Login/Index";
});

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddScoped<UserActionsFilter>();

var app = builder.Build();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var provider = scope.ServiceProvider;
    try
    {
        var context = provider.GetRequiredService<UserManagementDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminPanel}/{action=Index}/{id?}");

app.Run();