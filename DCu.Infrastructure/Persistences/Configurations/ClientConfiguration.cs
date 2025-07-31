using DCu.Domain.Entities;
using DCu.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCu.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.NameOrCompany)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.DocumentNumber)
            .HasMaxLength(30)
            .HasColumnName("DocumentNumber");

        builder.Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(c => c.Type)
            .HasConversion(
                t => t.Value,
                v => ClientType.From(v)
            )
            .HasColumnName("ClientType")
            .IsRequired();

        builder
            .OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Address)
                     .HasColumnName("Email")
                     .HasMaxLength(150)
                     .IsRequired(false);
            });

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);

        // Índices opcionales para unicidad
        builder.HasIndex(c => c.DocumentNumber).IsUnique();
        builder.OwnsOne(c => c.Email).HasIndex(e => e.Address);
    }
}

