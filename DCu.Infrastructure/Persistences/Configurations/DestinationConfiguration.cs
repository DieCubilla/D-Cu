using DCu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.ToTable("Destinations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
               .IsRequired();

        builder.Property(d => d.Order)
               .IsRequired();

        builder.Property(d => d.TripId)
               .IsRequired();

        builder.Property(d => d.AddressId)
               .IsRequired();

        builder.Property(d => d.Passengers);

        builder.Property(d => d.ArrivalAt);

        builder.Property(d => d.DepartureAt);

        builder.HasOne(d => d.Address)
               .WithMany()
               .HasForeignKey(d => d.AddressId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Trip)
               .WithMany(t => t.Destinations)
               .HasForeignKey(d => d.TripId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
