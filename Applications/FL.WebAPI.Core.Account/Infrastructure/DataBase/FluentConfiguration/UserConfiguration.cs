using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FL.WebAPI.Core.Account.Infrastructure.DataBase.FluentConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable(nameof(Domain.Entities.User));

            builder.Property(x => x.AcceptedConditions);
            builder.Property(x => x.AcceptedConditionsDate);
            builder.Property(x => x.RegistrationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}