using Clinic.Application.Queries.GetPatientDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;
    public PatientController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{idPatient:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatient(int idPatient, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetPatientDetailsQuery(idPatient), ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}