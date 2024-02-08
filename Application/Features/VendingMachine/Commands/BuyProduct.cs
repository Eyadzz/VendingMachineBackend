using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Features.VendingMachine.Commands;

public record BuyProduct : IRequest<BaseResponse>
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Amount is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public int Amount { get; set; }
}

public class BuyProductHandler : IRequestHandler<BuyProduct, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public BuyProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(BuyProduct request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
            return Responses.NotFound("Product");

        if (product.Quantity < request.Amount)
            return Responses.Custom("Not Enough Quantity", HttpStatusCode.BadRequest);

        var user = await _unitOfWork.Users.GetByIdAsync(_currentUserService.UserId);

        var cost = product.Price * request.Amount;
        if (user!.Balance < cost)
            return Responses.Custom("Insufficient Balance", HttpStatusCode.NotAcceptable);

        try
        {
            await _unitOfWork.BeginTransaction();
            
            product.Quantity -= request.Amount;
            user.Balance -= cost;

            await _unitOfWork.Commit();
            await _unitOfWork.Save();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }

        return Responses.Success($"You have successfully bought {request.Amount} {product.Name} for {cost}");
    }
}