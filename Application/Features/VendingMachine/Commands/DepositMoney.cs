using System.ComponentModel.DataAnnotations;

namespace Application.Features.VendingMachine.Commands;

public record DepositMoney : IRequest<BaseResponse>
{
    [Required(ErrorMessage = "Amount is required.")]
    [RegularExpression("^(10|25|50|100)$", ErrorMessage = "Amount must be 10, 25, 50, or 100.")]
    public int Amount { get; set; }
}

public class DepositMoneyHandler : IRequestHandler<DepositMoney, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DepositMoneyHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(DepositMoney request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_currentUserService.UserId);

        user!.Balance += request.Amount;

        await _unitOfWork.Save();

        return Responses.Success("Money Deposited");
    }
}