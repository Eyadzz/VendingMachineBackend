using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration.User;

public class UserConfig : IEntityTypeConfiguration<Domain.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.User.User> builder)
    {
        //builder.ToTable(t => t.HasCheckConstraint("CK_User_Balance", "Balance >= 0"));
    }
}