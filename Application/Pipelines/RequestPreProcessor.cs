using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Pipelines;

public class RequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestPreProcessor(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        return Task.Run(() => _logger.LogInformation("Request: [UserId:{@UserId}] [{@Request}]",
                 userId, request), cancellationToken);
    }
}

