using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> b)
    {
        b.ToTable("Patient");
        b.HasKey(p => p.IdPatient);
        b.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        b.Property(p => p.LastName).IsRequired().HasMaxLength(50);
        b.Property(p => p.BirthDate).IsRequired();
    }
}