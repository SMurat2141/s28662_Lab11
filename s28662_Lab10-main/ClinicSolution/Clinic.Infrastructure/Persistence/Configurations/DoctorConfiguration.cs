using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Persistence.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> b)
    {
        b.ToTable("Doctor");
        b.HasKey(d => d.IdDoctor);
        b.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
        b.Property(d => d.LastName).IsRequired().HasMaxLength(50);
        b.Property(d => d.Email).IsRequired().HasMaxLength(100);
    }
}