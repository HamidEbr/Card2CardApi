using System;

namespace Domain.Base.Exceptions
{
    public class BusinessException : BaseException
    {
        public Card2CardStatusCode Status { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string SourcePan { get; set; }
        public string DestinationPan { get; set; }
        public DateTime DueDate { get; set; }
        public string TrackingCode { get; set; }
        public string DigitalId { get; set; }

        public BusinessException()
        {
        }
    }
}
