using Domain.VendingMachine;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration.VendingMachine;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(u => u.Seller)
            .WithMany(u => u.Products)
            .HasForeignKey(u => u.SellerId);

        //builder.ToTable(t => t.HasCheckConstraint("CK_Product_Quantity", "Quantity >= 0"));
        //builder.ToTable(t => t.HasCheckConstraint("CK_Product_Price", "Price >= 0"));
    }
}