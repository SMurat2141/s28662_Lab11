using MediatR;

namespace Clinic.Application.Commands.AddPrescription;

public record PrescriptionItemDto(int MedicamentId, int Dose, string Description);

public record AddPrescriptionCommand(
    string PatientFirstName,
    string PatientLastName,
    DateOnly PatientBirthDate,
    int DoctorId,
    DateOnly Date,
    DateOnly DueDate,
    IReadOnlyCollection<PrescriptionItemDto> Items) : IRequest<int>;