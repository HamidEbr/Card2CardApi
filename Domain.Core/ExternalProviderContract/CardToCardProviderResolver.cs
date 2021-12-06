using Domain.Base.Exceptions;
using System.Collections.Generic;

namespace Domain.Core.ExternalProviderContract
{
    public class CardToCardProviderResolver : ICardToCardProviderResolver
    {
        private IEnumerable<ICard2CardProvider> _cardToCardProviders;

        public CardToCardProviderResolver(IEnumerable<ICard2CardProvider> cardToCardProviders)
        {
            _cardToCardProviders = cardToCardProviders;
        }

        public ICard2CardProvider GetCard2CardProvider(string pan)
        {
            foreach (var provider in _cardToCardProviders)
            {
                if (provider.IsValidPan(pan))
                    return provider;
            }
            throw new ProviderIsNotSupportedException();
        }
    }
}
