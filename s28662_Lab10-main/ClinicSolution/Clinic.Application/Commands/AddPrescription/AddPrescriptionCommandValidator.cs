using FluentValidation;

namespace Clinic.Application.Commands.AddPrescription;

public class AddPrescriptionCommandValidator : AbstractValidator<AddPrescriptionCommand>
{
    public AddPrescriptionCommandValidator()
    {
        RuleFor(c => c.Items)
            .NotEmpty()
            .Must(i => i.Count <= 10)
            .WithMessage("A prescription can contain up to 10 medicaments.");

        RuleFor(c => c.DueDate)
            .GreaterThanOrEqualTo(c => c.Date)
            .WithMessage("DueDate must be the same or later than Date.");
    }
}