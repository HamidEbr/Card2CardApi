using Domain.Base;
using Domain.Base.Exceptions;
using Domain.Core.ExternalProviderContract;
using Domain.Core.ExternalProviderContract.Request;
using Domain.Core.ExternalProviderContract.Response;
using Infrastructure.Card2CardProvider.MellatBank.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Card2CardProvider.MellatBank
{
    public class MellatBankCard2CardProvider : ICard2CardProvider
    {
        private readonly IOptionsMonitor<MellatServiceConfig> _serviceConfig;
        private static readonly Random _random = new();

        public decimal MaxTransferAmount => _serviceConfig.CurrentValue.MaxTransferAmount;

        public bool Enabled => _serviceConfig.CurrentValue.Enabled;

        public List<string> PanPrefixes => _serviceConfig.CurrentValue.PanPrefixes;

        public MellatBankCard2CardProvider(IOptionsMonitor<MellatServiceConfig> serviceConfig)
        {
            _serviceConfig = serviceConfig;
        }

        public Task<PanOwnerInfoResponse> GetPanOwnerInfoAsync(string pan)
        {
            throw new System.NotImplementedException();
        }

        public Task<InquiryResponse> InquiryAsync(InquiryRequest inquiryRequest)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidPan(string pan)
        {
            foreach (var panPrefix in PanPrefixes)
            {
                if (pan.StartsWith(panPrefix))
                    return true;
            }
            return false;
        }

        public async Task<Card2CardTransferResponse> Card2CardTransferAsync(Card2CardTransferRequest request)
        {
            var maxValue = (int)EnumExtensions.GetMaxValue<Card2CardStatusCode>();

            int randomStatusCode = _random.Next(0, maxValue + 10);
            Card2CardStatusCode statusCode = randomStatusCode < 10 ? Card2CardStatusCode.Successful : (Card2CardStatusCode)randomStatusCode - 10;
            var response = new Card2CardTransferResponse()
            {
                Status = statusCode,
                Description = statusCode.GetEnumDescription(),
                SourcePan = request.SourcePan,
                DestinationPan = request.DestinationPan,
                DueDate = DateTime.Now,
                Amount = request.Amount,
                TrackingCode = RandomCodeGenerator.RandomString(10),
                DigitalId = RandomCodeGenerator.RandomString(15),
            };
            if (statusCode != Card2CardStatusCode.Successful)
            {
                throw new BusinessException()
                {
                    Description = response.Description,
                    DueDate = response.DueDate,
                    Amount = response.Amount,
                    TrackingCode = response.TrackingCode,
                    DigitalId = response.DigitalId,
                };
            }
            return await Task.FromResult(response);
        }
    }
}
