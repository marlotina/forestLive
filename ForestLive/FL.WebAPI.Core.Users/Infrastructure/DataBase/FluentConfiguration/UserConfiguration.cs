using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FL.WebAPI.Core.Users.Infrastructure.DataBase.FluentConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable(nameof(Domain.Entities.User));

            builder.Property(x => x.Name);
            builder.Property(x => x.Surname);
            builder.Property(x => x.UrlWebSite);
            builder.Property(x => x.LastModification)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.LastLogin);
            builder.Property(x => x.IsCompany);
            builder.Property(x => x.AcceptedConditions);
            builder.Property(x => x.AcceptedConditionsDate);
            builder.Property(x => x.RegistrationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.LanguageId);
            builder.Property(x => x.Description);
            builder.Property(x => x.Photo);
            builder.Property(x => x.Location);
        }
    }
}