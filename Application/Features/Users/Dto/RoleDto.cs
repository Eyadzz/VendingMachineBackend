using Application.Mappings;
using Domain.User;

namespace Application.Features.Users.Dto;

public record RoleDto : BaseDto<Role,RoleDto>
{
    public byte Id { get; set; }
    public required string Name { get; set; }
}