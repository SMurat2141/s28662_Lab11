using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces;

public interface IClinicRepository
{
    Task<HashSet<int>> GetExistingMedicamentIdsAsync(IEnumerable<int> ids, CancellationToken ct);
    Task<bool> DoctorExistsAsync(int idDoctor, CancellationToken ct);
    Task<Patient?> GetPatientByNameAsync(string firstName, string lastName, CancellationToken ct);
    Task<Patient?> GetPatientDetailsAsync(int idPatient, CancellationToken ct);
    Task AddPrescriptionAsync(Prescription prescription, CancellationToken ct);
}