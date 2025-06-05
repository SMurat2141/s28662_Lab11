namespace Clinic.Application.Dtos;

public record PrescriptionDto(
    int IdPrescription,
    DateOnly Date,
    DateOnly DueDate,
    string DoctorFullName,
    IReadOnlyCollection<MedicamentDto> Medicaments);