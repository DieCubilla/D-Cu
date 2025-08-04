using DCu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Reference)
                .HasMaxLength(100);

        builder.Property(a => a.Street)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.StreetNumber)
               .HasMaxLength(10);

        builder.Property(a => a.CityId)
                .IsRequired();

        builder.HasOne(a => a.City)
               .WithMany()
               .HasForeignKey(a => a.CityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
