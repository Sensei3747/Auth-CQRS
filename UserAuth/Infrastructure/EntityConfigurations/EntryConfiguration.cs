using UserAuth.Domain.Entry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserAuth.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.ToTable("Entry");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
              .ValueGeneratedOnAdd();

        builder.Property(user => user.Name)
               .HasMaxLength(200);

        builder.Property(user => user.Email)
               .HasMaxLength(200);

        builder.Property(user => user.PasswordHash);

        builder.Property(user => user.CreatedOn)
               .HasDefaultValueSql("GetUtcDate()");

        builder.HasIndex(user => user.Email).IsUnique();
    }
}