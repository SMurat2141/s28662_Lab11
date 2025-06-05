namespace Clinic.Application.Dtos;

public record MedicamentDto(
    int IdMedicament,
    string Name,
    string Description,
    int Dose);