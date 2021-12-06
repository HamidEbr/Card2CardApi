using Domain.Core.ExternalProviderContract.Request;
using Domain.Core.ExternalProviderContract.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.ExternalProviderContract
{
    public interface ICard2CardProvider
    {
        decimal MaxTransferAmount { get; }
        bool Enabled { get; }
        List<string> PanPrefixes { get; }
        Task<PanOwnerInfoResponse> GetPanOwnerInfoAsync(string pan);
        Task<Card2CardTransferResponse> Card2CardTransferAsync(Card2CardTransferRequest submitTransferRequest);
        Task<InquiryResponse> InquiryAsync(InquiryRequest inquiryRequest);
        bool IsValidPan(string pan);
    }
}
