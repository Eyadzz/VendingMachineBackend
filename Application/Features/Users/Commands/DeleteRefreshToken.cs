using Application.Common.Tokens;

namespace Application.Features.Users.Commands;

public record DeleteRefreshToken(string Token) : IRequest<BaseResponse>;

public class DeleteRefreshTokenHandler : IRequestHandler<DeleteRefreshToken, BaseResponse>
{
    private readonly ICacheService _cache;
    private readonly ICurrentUserService _currentUserService;

    public DeleteRefreshTokenHandler(ICacheService cache, ICurrentUserService currentUserService)
    {
        _cache = cache;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponse> Handle(DeleteRefreshToken request, CancellationToken cancellationToken)
    {
        var userId = await _cache.Get(Tokens.Refresh, request.Token);
        if(userId == null)
            return Responses.Invalid("Token is Invalid");
        
        if (int.Parse(userId) != _currentUserService.UserId)
            return Responses.Unauthorized();
        
        await _cache.Remove(Tokens.Refresh, request.Token);

        return Responses.Success("Token Deleted");
    }
}