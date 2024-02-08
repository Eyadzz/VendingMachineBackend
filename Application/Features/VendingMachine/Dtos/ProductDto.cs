using Application.Mappings;
using Domain.VendingMachine;

namespace Application.Features.VendingMachine.Dtos;

public record ProductDto : BaseDto<Product, ProductDto>
{
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}