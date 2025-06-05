using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Persistence.Configurations;

public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> b)
    {
        b.ToTable("Medicament");
        b.HasKey(m => m.IdMedicament);
        b.Property(m => m.Name).IsRequired().HasMaxLength(100);
        b.Property(m => m.Description).HasMaxLength(200);
        b.Property(m => m.Type).HasMaxLength(50);
    }
}