using FluentValidation;

namespace Application.Command.Validation
{
    public class Card2CardRequestCommandValidator : BaseCommandValidator<Card2CardCommand>
    {
        public Card2CardRequestCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(10000).WithMessage("MaxAmount should be greater than 10000");

            RuleFor(x => x.SourcePan)
                .NotNull().WithMessage("SourcePan is required").NotEmpty().WithMessage("SourcePan is required").Must(IsValidPan).WithMessage("SourcePan is not valid");

            RuleFor(x => x.DestinationPan)
                .NotNull().WithMessage("DestinationPan is required").NotEmpty().WithMessage("DestinationPan is required").Must(IsValidPan).WithMessage("DestinationPan is not valid");

            RuleFor(x => x.MobileNumber)
                .NotNull().WithMessage("MobileNumber is required").NotEmpty().WithMessage("MobileNumber is required").Must(IsValidMobileNumber).WithMessage("MobileNumber is not valid");

            RuleFor(x => x.OTP)
                .NotNull().WithMessage("OTP is required").NotEmpty().WithMessage("OTP is required");

            RuleFor(x => x.CVV2)
                .NotNull().WithMessage("CVV2 is required").NotEmpty().WithMessage("CVV2 is required").Must(IsValidCVV2).WithMessage("CVV2 is not valid");

            RuleFor(x => x.ExpYear)
                .NotNull().WithMessage("ExpYear is required").NotEmpty().WithMessage("ExpYear is required").Must(IsValidExpYear).WithMessage("ExpYear is not valid");

            RuleFor(x => x.ExpMonth)
                .NotNull().WithMessage("ExpMonth is required").NotEmpty().WithMessage("ExpMonth is required").Must(IsValidExpMonth).WithMessage("ExpMonth is not valid");
        }
    }
}
