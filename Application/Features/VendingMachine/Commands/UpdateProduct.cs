using System.ComponentModel.DataAnnotations;

namespace Application.Features.VendingMachine.Commands;

public record UpdateProduct : IRequest<BaseResponse>
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public float Price { get; set; }
    
    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int QuantityToBeAdded { get; set; }
}

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