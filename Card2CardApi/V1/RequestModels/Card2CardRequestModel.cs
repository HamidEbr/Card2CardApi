namespace Card2CardApi.Service.V1.RequestModels
{
    public class Card2CardRequestModel
    {
        public long Amount { get; set; }
        public string SourcePan { get; set; }
        public string DestinationPan { get; set; }
        public string CVV2 { get; set; }
        public string OTP { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
        public string MobileNumber { get; set; }
    }
}
