using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class AbstractController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected AbstractController(IMediator mediator) => Mediator = mediator;
}