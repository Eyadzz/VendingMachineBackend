using Application.Mappings;
using Domain.User;

namespace Application.Features.Users.Dto;

public record UserDto : BaseDto<User,UserDto>
{
    public int Id { get; set; }
    public required string Username { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public float Balance { get; set; }
    public required string Role { get; set; }

    protected override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(dest => dest.Role, src => src.Role.Name);
    }
}