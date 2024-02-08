using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Features.VendingMachine.Commands;
using Application.Features.VendingMachine.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Buyer")]
public class VendingMachineController : AbstractController
{
    public VendingMachineController(IMediator mediator) : base(mediator)
    {
    }
    
    /// <summary>
    /// Retrieves the list of available products from the vending machine.
    /// </summary>
    /// <returns>An action result containing the list of products.</returns>
    [AllowAnonymous]
    [HttpGet("Products")]
    public async Task<IActionResult> Products()
    {
        var response = await Mediator.Send(new Products());
        return StatusCode(response.StatusCode, response);
    }
        
    /// <summary>
    /// Buys a product from the vending machine.
    /// </summary>
    /// <param name="request">The request containing the product information to buy.</param>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpPost("Buy")]
    public async Task<IActionResult> Buy(BuyProduct request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
        
    /// <summary>
    /// Deposits money into the vending machine.
    /// </summary>
    /// <param name="request">The request containing the amount of money to deposit.</param>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpPost("Deposit")]
    public async Task<IActionResult> Deposit(DepositMoney request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
        
    /// <summary>
    /// Resets the deposit amount in the vending machine.
    /// </summary>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpPost("Reset")]
    public async Task<IActionResult> Reset()
    {
        var response = await Mediator.Send(new ResetDeposit());
        return StatusCode(response.StatusCode, response);
    }
}