using Application.Features.VendingMachine.Dtos;

namespace Application.Features.VendingMachine.Queries;

public record Products : IRequest<BaseResponse>;

public class ProductsHandler : IRequestHandler<Products, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> Handle(Products request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.ListAllAsync();

        return Responses.Success(products.Adapt<List<ProductDto>>());
    }
}