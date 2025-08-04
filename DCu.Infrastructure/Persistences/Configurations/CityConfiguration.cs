using DCu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .IsRequired();

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.DepartmentId)
                .IsRequired();

        builder.HasOne(c => c.Department)
               .WithMany(d => d.Cities)
               .HasForeignKey(c => c.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
