using Application.Features.VendingMachine.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Seller")]
public class VendingMachineSellerController : AbstractController
{
    public VendingMachineSellerController(IMediator mediator) : base(mediator) {}
    
    /// <summary>
    /// Adds a new product to the vending machine.
    /// </summary>
    /// <param name="request">The request containing the product information to add.</param>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpPost("Add")]
    public async Task<IActionResult> AddProduct(AddProduct request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Updates an existing product in the vending machine.
    /// </summary>
    /// <param name="request">The request containing the updated product information.</param>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProduct(UpdateProduct request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Deletes a product from the vending machine.
    /// </summary>
    /// <param name="request">The request containing the product information to delete.</param>
    /// <returns>HTTP status indicating the result of the operation.</returns>
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteProduct(DeleteProduct request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
}