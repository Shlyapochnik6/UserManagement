using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.User.Removing;
using UserManagement.Application.Queries.User.GetListUsers;
using UserManagement.Domain;

namespace UserManagement.MVC.Controllers;

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
}