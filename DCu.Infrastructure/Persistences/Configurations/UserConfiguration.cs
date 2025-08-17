using DCu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCu.Infrastructure.Persistences.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired();

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

        // PasswordHash (requerido)
        builder.OwnsOne(u => u.PasswordHash, pw =>
        {
            pw.Property(p => p.Value)
              .HasColumnName("PasswordHash")
              .IsRequired();
        });

        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .IsRequired();
    }
}

