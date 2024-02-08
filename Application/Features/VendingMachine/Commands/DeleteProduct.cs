namespace Application.Features.VendingMachine.Commands;

public record DeleteProduct(int Id) : IRequest<BaseResponse>;

public class DeleteProductHandler : IRequestHandler<DeleteProduct, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
            return Responses.NotFound("Product");

        if (product.SellerId != _currentUserService.UserId)
            return Responses.Unauthorized();

        _unitOfWork.Products.Delete(product);

        await _unitOfWork.Save();

        return Responses.Success("Product Deleted");
    }
}