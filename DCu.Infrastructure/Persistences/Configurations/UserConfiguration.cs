using DCu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCu.Infrastructure.Persistences.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.IsActive)
            .IsRequired();

        // Email (no requerido)
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(150)
                .IsRequired(false); // Email es opcional
        });

        // Password (requerido)
        builder.OwnsOne(u => u.Password, pw =>
        {
            pw.Property(p => p.HashedValue)
              .HasColumnName("PasswordHash")
              .IsRequired();
        });

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .IsRequired();
    }
}

