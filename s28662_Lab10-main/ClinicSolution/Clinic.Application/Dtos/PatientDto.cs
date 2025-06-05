namespace Clinic.Application.Dtos;

public record PatientDto(
    int IdPatient,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    IReadOnlyCollection<PrescriptionDto> Prescriptions);