using System.ComponentModel.DataAnnotations;
using Domain.VendingMachine;

namespace Application.Features.VendingMachine.Commands;

public record AddProduct : IRequest<BaseResponse>
{
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public float Price { get; set; }
    
    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
}

public class AddProductHandler : IRequestHandler<AddProduct, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public AddProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(AddProduct request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            SellerId = _currentUserService.UserId
        };

        await _unitOfWork.Products.AddAsync(product);

        await _unitOfWork.Save();

        return Responses.Success("Product Added");
    }
}