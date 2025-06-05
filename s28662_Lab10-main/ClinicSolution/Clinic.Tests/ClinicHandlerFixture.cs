using AutoMapper;
using Clinic.Api.Profiles;
using Clinic.Application.Commands.AddPrescription;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Clinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Tests;

public class ClinicHandlerFixture : IDisposable
{
    public ClinicDbContext Context { get; }
    public AddPrescriptionCommandHandler Handler { get; }

    public ClinicHandlerFixture()
    {
        var opts = new DbContextOptionsBuilder<ClinicDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
        Context = new ClinicDbContext(opts);

        Seed(Context);

        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var mapper = mapperConfig.CreateMapper();

        var repo = new ClinicRepository(Context);
        var validator = new AddPrescriptionCommandValidator();
        Handler = new AddPrescriptionCommandHandler(repo, mapper, validator);
    }

    private static void Seed(ClinicDbContext ctx)
    {
        ctx.Medicaments.AddRange(
            new Medicament { IdMedicament = 1, Name = "A", Description = "", Type = "Tab" },
            new Medicament { IdMedicament = 2, Name = "B", Description = "", Type = "Tab" },
            new Medicament { IdMedicament = 3, Name = "C", Description = "", Type = "Tab" }
        );

        ctx.Doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Doc", LastName = "Tor", Email = "doc@example.com" });

        ctx.SaveChanges();
    }

    public AddPrescriptionCommand ValidCommand() => new(
        PatientFirstName: "John",
        PatientLastName: "Smith",
        PatientBirthDate: new DateOnly(1990, 1, 1),
        DoctorId: 1,
        Date: DateOnly.FromDateTime(DateTime.Today),
        DueDate: DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
        Items: new List<PrescriptionItemDto> { new(1, 1, "Once daily") });

    public void Dispose() => Context.Dispose();
}