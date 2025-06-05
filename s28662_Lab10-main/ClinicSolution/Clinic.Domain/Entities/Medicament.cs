namespace Clinic.Domain.Entities;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}