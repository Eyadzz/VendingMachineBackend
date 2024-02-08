namespace Application.Features.Users.Commands;

public record DeleteUser : IRequest<BaseResponse>;

public class DeleteUserHandler : IRequestHandler<DeleteUser, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteUserHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_currentUserService.UserId);

        _unitOfWork.Users.Delete(user!);

        await _unitOfWork.Save();

        return Responses.Success("User Deleted");
    }
}