namespace Clinic.Domain.Entities;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = default!;

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();

    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}