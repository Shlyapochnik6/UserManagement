using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Queries.User.Login;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers;

public class LoginController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LoginController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var query = _mapper.Map<LoginUserQuery>(model);
        query.State = ModelState;
        var state = await _mediator.Send(query);
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        return RedirectToAction("Index", "AdminPanel");
    }
}