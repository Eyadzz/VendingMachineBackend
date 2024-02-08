namespace Domain.VendingMachine;

[Table("Products", Schema = "VendingMachine")]
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required float Price { get; set; }
    public required int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int SellerId { get; set; }
    
    public User.User Seller { get; set; } = null!;
}