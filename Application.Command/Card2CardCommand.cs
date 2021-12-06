using Domain.Base.Exceptions;
using Domain.Core.ExternalProviderContract;
using Domain.Core.ExternalProviderContract.Request;
using Domain.Core.ExternalProviderContract.Response;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Command
{
    public class Card2CardCommand : BaseCommand<Card2CardTransferResponse>
    {
        public long Amount { get; set; }
        public string SourcePan { get; set; }
        public string DestinationPan { get; set; }
        public string CVV2 { get; set; }
        public string OTP { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
        public string MobileNumber { get; set; }

        public Card2CardCommand()
        {
        }
    }

    public class Card2CardCommandHandler : BaseCommandHandler<Card2CardCommand, Card2CardTransferResponse>
    {
        private readonly ICardToCardProviderResolver _resolver;
        private readonly IValidator<Card2CardCommand> _validator;

        public Card2CardCommandHandler(ICardToCardProviderResolver resolver, IValidator<Card2CardCommand> validator)
        {
            _resolver = resolver;
            _validator = validator;
        }

        public override async Task<Card2CardTransferResponse> Handle(Card2CardCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            if (request.SourcePan == request.DestinationPan)
            {
                throw new SourcePanEqualsDestinationPanException();
            }
            var provider = _resolver.GetCard2CardProvider(request.SourcePan);
            Card2CardTransferResponse response = await provider.Card2CardTransferAsync(new Card2CardTransferRequest()
            {
                Amount = request.Amount,
                CVV2 = request.CVV2,
                DestinationPan = request.DestinationPan,
                ExpMonth = request.ExpMonth,
                ExpYear = request.ExpYear,
                MobileNumber = request.MobileNumber,
                OTP = request.OTP,
                SourcePan = request.SourcePan,
            });
            return response;
        }
    }
}
