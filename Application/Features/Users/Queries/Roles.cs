using Application.Features.Users.Dto;

namespace Application.Features.Users.Queries;

public record Roles : IRequest<BaseResponse>;

public class RolesHandler : IRequestHandler<Roles, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BaseResponse> Handle(Roles request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.Roles.ListAllAsync();

        return Responses.Success(roles.Adapt<List<RoleDto>>());
    }
}