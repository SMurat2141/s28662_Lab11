namespace Clinic.Domain.Entities;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName  { get; set; } = default!;
    public DateOnly BirthDate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}