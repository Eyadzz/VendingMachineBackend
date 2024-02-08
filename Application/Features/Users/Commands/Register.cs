using System.ComponentModel.DataAnnotations;
using Application.Contracts.Authentication;
using Application.Mappings;
using Domain.User;

namespace Application.Features.Users.Commands;

public record Register : BaseDto<User,Register>, IRequest<BaseResponse>
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[\\W_])(?!.*\\s).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "Role is required")]
    public int RoleId { get; set; }
}

public class RegisterHandler : IRequestHandler<Register, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<BaseResponse> Handle(Register request, CancellationToken cancellationToken)
    {
        var emailExists = await _unitOfWork.Users.UsernameExists(request.Username);
        if (emailExists)
            return Responses.AlreadyExist("Username");
        
        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
            return Responses.NotFound("Role");
        
        var user = request.Adapt<User>();
        user.Password = _passwordHasher.Hash(request.Password);
        

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.Save();
        
        return Responses.Success("Account Created");
    }
}