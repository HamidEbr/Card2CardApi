using Domain.Base;
using System;

namespace Card2CardApi.Service.V1.ResponseModels
{
    public class Card2CardResponseModel
    {
        public Card2CardStatusCode Status { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public long TrackingCode { get; set; }
        public long DigitalId { get; set; }
        public string SourcePan { get; set; }
        public string DestinationPan { get; set; }
        public DateTime DouDate { get; set; }
    }
}
