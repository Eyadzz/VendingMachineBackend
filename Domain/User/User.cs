using Domain.VendingMachine;

namespace Domain.User;


[Table("Users", Schema = "Users")]
public class User
{
    public int Id { get; set; }
    public required string Username { get; set; } 
    public required string Password { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public float Balance { get; set; } = 0f;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}