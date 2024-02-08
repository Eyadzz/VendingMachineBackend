namespace Application.Features.VendingMachine.Commands;

public record UpdateProduct(int Id, string Name, float Price, int QuantityToBeAdded) : IRequest<BaseResponse>;

public class UpdateProductHandler : IRequestHandler<UpdateProduct, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
            return Responses.NotFound("Product");

        if (product.SellerId != _currentUserService.UserId)
            return Responses.Unauthorized();

        product.Name = request.Name;
        product.Price = request.Price;
        product.Quantity += request.QuantityToBeAdded;
        
        product.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Save();

        return Responses.Success("Product Updated");
    }
}