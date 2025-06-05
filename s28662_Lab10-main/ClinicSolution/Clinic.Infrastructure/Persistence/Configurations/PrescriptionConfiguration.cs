using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Persistence.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> b)
    {
        b.ToTable("Prescription");
        b.HasKey(p => p.IdPrescription);
        b.Property(p => p.Date).IsRequired();
        b.Property(p => p.DueDate).IsRequired();
        b.Property(p => p.RowVersion).IsRowVersion();

        b.HasOne(p => p.Doctor)
         .WithMany(d => d.Prescriptions)
         .HasForeignKey(p => p.DoctorId);

        b.HasOne(p => p.Patient)
         .WithMany(pa => pa.Prescriptions)
         .HasForeignKey(p => p.PatientId);
    }
}