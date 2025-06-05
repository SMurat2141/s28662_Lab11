using Clinic.Application.Dtos;
using MediatR;

namespace Clinic.Application.Queries.GetPatientDetails;

public record GetPatientDetailsQuery(int IdPatient) : IRequest<PatientDto?>;