using Application.Features.Users.Dto;

namespace Application.Features.Users.Queries;

public record UserData : IRequest<BaseResponse>;

public class UserDataHandler : IRequestHandler<UserData, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UserDataHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(UserData request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetDetails(_currentUserService.UserId);

        return Responses.Success(user.Adapt<UserDto>());
    }
}