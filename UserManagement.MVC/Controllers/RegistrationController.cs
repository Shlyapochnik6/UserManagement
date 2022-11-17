﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.User.Registration;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers;

public class RegistrationController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegistrationController(IMapper mapper, IMediator mediator)
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
    public async Task<IActionResult> Index(RegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var command = _mapper.Map<CreateUserCommand>(model);
        var createUser = await _mediator.Send(command);
        if (createUser == false)
        {
            return View(model);
        }
        return RedirectToAction("Index", "Home");
    }
}