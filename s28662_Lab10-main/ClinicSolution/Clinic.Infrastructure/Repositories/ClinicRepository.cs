using Clinic.Application.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;

public class ClinicRepository : IClinicRepository
{
    private readonly ClinicDbContext _ctx;
    public ClinicRepository(ClinicDbContext ctx) => _ctx = ctx;

    public async Task<HashSet<int>> GetExistingMedicamentIdsAsync(IEnumerable<int> ids, CancellationToken ct) =>
        (await _ctx.Medicaments
            .Where(m => ids.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync(ct))
        .ToHashSet();

    public Task<bool> DoctorExistsAsync(int idDoctor, CancellationToken ct)
        => _ctx.Doctors.AnyAsync(d => d.IdDoctor == idDoctor, ct);

    public Task<Patient?> GetPatientByNameAsync(string firstName, string lastName, CancellationToken ct)
        => _ctx.Patients.SingleOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName, ct);

    public Task<Patient?> GetPatientDetailsAsync(int idPatient, CancellationToken ct)
        => _ctx.Patients
               .Include(p => p.Prescriptions.OrderBy(pr => pr.DueDate))
                   .ThenInclude(pr => pr.Doctor)
               .Include(p => p.Prescriptions)
                   .ThenInclude(pr => pr.PrescriptionMedicaments)
                       .ThenInclude(pm => pm.Medicament)
               .SingleOrDefaultAsync(p => p.IdPatient == idPatient, ct);

    public async Task AddPrescriptionAsync(Prescription prescription, CancellationToken ct)
    {
        _ctx.Prescriptions.Add(prescription);
        await _ctx.SaveChangesAsync(ct);
    }
}