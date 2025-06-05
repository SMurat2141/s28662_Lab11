using AutoMapper;
using Clinic.Application.Dtos;
using Clinic.Application.Interfaces;
using MediatR;

namespace Clinic.Application.Queries.GetPatientDetails;

public class GetPatientDetailsQueryHandler : IRequestHandler<GetPatientDetailsQuery,PatientDto?>
{
    private readonly IClinicRepository _repo;
    private readonly IMapper _mapper;
    public GetPatientDetailsQueryHandler(IClinicRepository repo, IMapper mapper)
        => (_repo, _mapper) = (repo, mapper);

    public async Task<PatientDto?> Handle(GetPatientDetailsQuery q, CancellationToken ct)
    {
        var patient = await _repo.GetPatientDetailsAsync(q.IdPatient, ct);
        return patient is null ? null : _mapper.Map<PatientDto>(patient);
    }
}