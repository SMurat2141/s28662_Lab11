using Clinic.Application.Commands.AddPrescription;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IMediator _mediator;
    public PrescriptionController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> IssuePrescription([FromBody] AddPrescriptionCommand command, CancellationToken ct)
    {
        int id = await _mediator.Send(command, ct);
        return Created(string.Empty, new { idPrescription = id });
    }
}