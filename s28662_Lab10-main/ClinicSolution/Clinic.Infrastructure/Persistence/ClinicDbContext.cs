using Microsoft.EntityFrameworkCore;
using Clinic.Domain.Entities;

namespace Clinic.Infrastructure.Persistence;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

    public DbSet<Patient> Patients              => Set<Patient>();
    public DbSet<Doctor> Doctors                => Set<Doctor>();
    public DbSet<Medicament> Medicaments        => Set<Medicament>();
    public DbSet<Prescription> Prescriptions    => Set<Prescription>();
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments => Set<PrescriptionMedicament>();

    public DbSet<User> Users                    => Set<User>();
    public DbSet<RefreshToken> RefreshTokens    => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(cfg =>
        {
            cfg.HasIndex(u => u.Username).IsUnique();
            cfg.Property(u => u.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<RefreshToken>(cfg =>
        {
            cfg.HasIndex(t => t.Token).IsUnique();
            cfg.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UserId);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
    }
}