using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ErrorController : AbstractController
{
    public ErrorController(IMediator mediator) : base(mediator) {}
    
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, title) = exception switch
        {
            BaseException baseException => (baseException.StatusCode, baseException.Message),
            _ => (500, "Something went wrong!")
        };
        
        return Problem(
            statusCode: statusCode,
            title: title,
            detail: exception?.ToString()
        );
    }
}