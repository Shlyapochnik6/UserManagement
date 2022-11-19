using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.User.Blocking;
using UserManagement.Application.Commands.User.Removing;
using UserManagement.Application.Commands.User.Unblocking;
using UserManagement.Application.Queries.User.GetListUsers;
using UserManagement.Domain;
using UserManagement.MVC.FilterAttributes;

namespace UserManagement.MVC.Controllers;

[ServiceFilter(typeof(UserActionsFilter))]
[Authorize]
public class AdminPanelController : BaseController
{
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;

    public AdminPanelController(IMediator mediator,
        SignInManager<User> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = new GetListUsersQuery();
        var users = await _mediator.Send(query);
        
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromBody] long[] selectedUsers)
    {
        var command = new RemoveUserCommand
        {
            SelectedUsers = selectedUsers,
            CurrentUserId = UserId
        };
        await _mediator.Send(command);
        return RedirectToAction("Index", "AdminPanel");
    }
    
    [HttpPost]
    public async Task<IActionResult> Block([FromBody] long[] selectedUsers)
    {
        var command = new BlockUserCommand
        {
            CurrentUserId = UserId,
            SelectedUsers = selectedUsers
        };
        await _mediator.Send(command);
        return RedirectToAction("Index", "AdminPanel");
    }
    
    [HttpPost]
    public async Task<IActionResult> Unblock([FromBody] long[] selectedUsers)
    {
        var command = new UnblockUserCommand { SelectedUsers = selectedUsers };
        await _mediator.Send(command);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Login");
    }
}