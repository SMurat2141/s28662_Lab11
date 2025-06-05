using AutoMapper;
using Clinic.Domain.Entities;
using Clinic.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Clinic.Application.Commands.AddPrescription;

public class AddPrescriptionCommandHandler : IRequestHandler<AddPrescriptionCommand,int>
{
    private readonly IClinicRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPrescriptionCommand> _validator;

    public AddPrescriptionCommandHandler(IClinicRepository repo, IMapper mapper, IValidator<AddPrescriptionCommand> validator)
        => (_repo, _mapper, _validator) = (repo, mapper, validator);

    public async Task<int> Handle(AddPrescriptionCommand c, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(c, ct);

        var uniqMedIds = c.Items.Select(i => i.MedicamentId).Distinct().ToList();
        var existingMedIds = await _repo.GetExistingMedicamentIdsAsync(uniqMedIds, ct);
        var missing = uniqMedIds.Except(existingMedIds).ToList();
        if (missing.Any())
            throw new ValidationException($"Unknown medicament IDs: {string.Join(", ", missing)}");

        if (!await _repo.DoctorExistsAsync(c.DoctorId, ct))
            throw new ValidationException($"Unknown doctor ID: {c.DoctorId}");

        var patient = await _repo.GetPatientByNameAsync(c.PatientFirstName, c.PatientLastName, ct)
                     ?? new Patient
                     {
                         FirstName = c.PatientFirstName,
                         LastName  = c.PatientLastName,
                         BirthDate = c.PatientBirthDate
                     };

        var prescription = new Prescription
        {
            Date    = c.Date,
            DueDate = c.DueDate,
            DoctorId = c.DoctorId,
            PrescriptionMedicaments = c.Items.Select(i => new PrescriptionMedicament
            {
                IdMedicament = i.MedicamentId,
                Dose = i.Dose,
                Description = i.Description
            }).ToList()
        };

        patient.Prescriptions.Add(prescription);
        await _repo.AddPrescriptionAsync(prescription, ct);

        return prescription.IdPrescription;
    }
}