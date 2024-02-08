using Domain.User;
using Domain.VendingMachine;

namespace Persistence;

public abstract class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Buyer" },
            new Role { Id = 2, Name = "Seller" }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Buyer",
                RoleId = 1,
                Balance = 100,
                Password = "$2a$11$Pjl9qxNSCv9rSUp1W5/Tb..q/fHcEAkiG5tWK2sosV8onQ91kyivq"
            },
            new User
            {
                Id = 2,
                Username = "Seller",
                RoleId = 2,
                Balance = 0,
                Password = "$2a$11$Pjl9qxNSCv9rSUp1W5/Tb..q/fHcEAkiG5tWK2sosV8onQ91kyivq"
            }
        );
        
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Chips", Price = 5, Quantity = 100, SellerId = 2 },
            new Product { Id = 2, Name = "Coke", Price = 11, Quantity = 30, SellerId = 2 },
            new Product { Id = 3, Name = "Biscuits", Price = 2.5f, Quantity = 56, SellerId = 2 }
        );
    }
}