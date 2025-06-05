namespace Clinic.Domain.Entities;

public class PrescriptionMedicament
{
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; } = default!;

    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; } = default!;

    public int Dose { get; set; }
    public string Description { get; set; } = string.Empty;
}