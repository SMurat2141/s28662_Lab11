using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Persistence.Configurations;

public class PrescriptionMedicamentConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> b)
    {
        b.ToTable("Prescription_Medicament");
        b.HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        b.Property(pm => pm.Dose).IsRequired();
        b.Property(pm => pm.Description).HasMaxLength(100);

        b.HasOne(pm => pm.Prescription)
         .WithMany(p => p.PrescriptionMedicaments)
         .HasForeignKey(pm => pm.IdPrescription);

        b.HasOne(pm => pm.Medicament)
         .WithMany(m => m.PrescriptionMedicaments)
         .HasForeignKey(pm => pm.IdMedicament);
    }
}