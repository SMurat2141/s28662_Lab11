namespace Clinic.Domain.Entities;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName  { get; set; } = default!;
    public string Email { get; set; } = default!;

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}