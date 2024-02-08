
namespace Domain.User;

[Table("Roles", Schema = "Users")]
public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<User> Users { get; set; } = null!;
}