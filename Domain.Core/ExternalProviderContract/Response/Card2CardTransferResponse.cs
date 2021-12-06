using Domain.Base;
using System;

namespace Domain.Core.ExternalProviderContract.Response
{
    public class Card2CardTransferResponse
    {
        public Card2CardStatusCode Status { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public string TrackingCode { get; set; }
        public string DigitalId { get; set; }
        public string SourcePan { get; set; }
        public string DestinationPan { get; set; }
        public DateTime DueDate { get; set; }
    }
}
