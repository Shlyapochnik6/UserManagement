using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Queries.User.GetListUsers;
using UserManagement.Domain;

namespace UserManagement.MVC.Controllers;

public class AdminPanelController : Controller
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
    
    
}