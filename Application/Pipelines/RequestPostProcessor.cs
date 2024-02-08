using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Pipelines;

public class RequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestPostProcessor(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        return Task.Run(() => _logger.LogInformation("Response: [UserId:{@UserId}] [{@Response}]",
            userId, response), cancellationToken);
    }
}