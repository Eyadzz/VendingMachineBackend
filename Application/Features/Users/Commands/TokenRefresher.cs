using Application.Common;
using Application.Common.Tokens;
using Application.Contracts.Authentication;
using Application.Features.Users.Dto;

namespace Application.Features.Users.Commands;

public record TokenRefresher(string RefreshToken) : IRequest<BaseResponse>;


public class TokenRefresherHandler : IRequestHandler<TokenRefresher, BaseResponse>
{
    private readonly IAuthProvider _authProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;

    public TokenRefresherHandler(IAuthProvider authProvider, IUnitOfWork unitOfWork, ICacheService cache)
    {
        _authProvider = authProvider;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<BaseResponse> Handle(TokenRefresher request, CancellationToken cancellationToken)
    {
        var userId = await _cache.Get(Tokens.Refresh, request.RefreshToken);

        if (userId is null)
            return Responses.Invalid("Invalid Token.");
        
        var user = await _unitOfWork.Users.GetDetails(Convert.ToInt32(userId));
        
        var response = new LoginResponse(AccessToken: _authProvider.AccessToken(user), RefreshToken: request.RefreshToken);

        return Responses.Success(response);
    }
}