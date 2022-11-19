using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.Application.Queries.User.Constants;
using UserManagement.Application.Queries.User.GetUserQuery;
using UserManagement.Domain;

namespace UserManagement.MVC.FilterAttributes;

public class UserActionsFilter : Attribute, IAsyncActionFilter
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMediator _mediator;

    public UserActionsFilter(SignInManager<User> signInManager, IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    public async System.Threading.Tasks.Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var controller = (Controller)context.Controller;
        var claimsPrincipal = context.HttpContext.User;

        var currentUserId = Convert.ToInt64(claimsPrincipal
            .FindFirstValue(ClaimTypes.NameIdentifier));

        var query = new GetUserQuery { UserId = currentUserId };
        var user = await _mediator.Send(query);

        if (user is null || user.Status == AccountStatus.Blocked)
        {
            await _signInManager.SignOutAsync();

            context.Result = controller.RedirectToAction("Index", "Login");
            return;
        }

        await next();
    }
}