namespace Domain.Core.ExternalProviderContract
{
    public interface ICardToCardProviderResolver
    {
        ICard2CardProvider GetCard2CardProvider(string pan);
    }
}
