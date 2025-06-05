using FluentAssertions;
using Xunit;

namespace Clinic.Tests.Application.Commands;

public class AddPrescriptionCommandHandlerTests : IClassFixture<ClinicHandlerFixture>
{
    private readonly ClinicHandlerFixture _fx;
    public AddPrescriptionCommandHandlerTests(ClinicHandlerFixture fx) => _fx = fx;

    [Fact]
    public async Task Handler_Succeeds_With_Valid_Command()
    {
        var cmd = _fx.ValidCommand();

        int id = await _fx.Handler.Handle(cmd, CancellationToken.None);

        id.Should().BeGreaterThan(0);
        (await _fx.Context.Prescriptions.FindAsync(id)).Should().NotBeNull();
    }
}