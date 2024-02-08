namespace Application.Features.VendingMachine.Commands;

public record ResetDeposit : IRequest<BaseResponse>;

public class ResetDepositHandler : IRequestHandler<ResetDeposit, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ResetDepositHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(ResetDeposit request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_currentUserService.UserId);

        user!.Balance = 0;

        await _unitOfWork.Save();

        return Responses.Success("Deposit Reset");
    }
}