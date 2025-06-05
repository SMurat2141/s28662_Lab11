using FluentAssertions;
using FluentValidation;
using Xunit;
using Clinic.Application.Commands.AddPrescription;

namespace Clinic.Tests.Application.Commands;

public class InvalidMedicamentTests : IClassFixture<ClinicHandlerFixture>
{
    private readonly ClinicHandlerFixture _fx;
    public InvalidMedicamentTests(ClinicHandlerFixture fx) => _fx = fx;

    [Fact]
    public async Task Handler_Fails_When_Medicament_Missing()
    {
        var baseCmd = _fx.ValidCommand();
        var alteredItems = baseCmd.Items.Append(new PrescriptionItemDto(999, 1, "Ghost")).ToList();
        var cmd = baseCmd with { Items = alteredItems };

        Func<Task> act = () => _fx.Handler.Handle(cmd, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>()
                 .WithMessage("*999*");
    }
}