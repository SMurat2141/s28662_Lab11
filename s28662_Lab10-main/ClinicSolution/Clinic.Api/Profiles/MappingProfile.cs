using AutoMapper;
using Clinic.Application.Dtos;
using Clinic.Domain.Entities;

namespace Clinic.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(d => d.Prescriptions,
                opt => opt.MapFrom(s => s.Prescriptions.OrderBy(pr => pr.DueDate)));

        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.DoctorFullName,
                opt => opt.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(d => d.Medicaments,
                opt => opt.MapFrom(s => s.PrescriptionMedicaments));

        CreateMap<PrescriptionMedicament, MedicamentDto>()
            .ConstructUsing(pm => new MedicamentDto(
                pm.IdMedicament,
                pm.Medicament.Name,
                pm.Description,
                pm.Dose));
    }
}