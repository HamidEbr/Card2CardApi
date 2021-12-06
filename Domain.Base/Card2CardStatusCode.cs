using System.ComponentModel;

namespace Domain.Base
{
    public enum Card2CardStatusCode
    {
        [Description("خطای نامشخص")]
        Unknown = 0,
        [Description("عملیات موفقیت آمیز بود")]
        Successful = 1,
        [Description("رمز کارت نامعتبر است")]
        InvalidPassword = 2,
        [Description("شماره کار مبدا غیرفعال است")]
        SourcePanWasDeactive = 3,
        [Description("شماره کار مقصد غیرفعال است")]
        DestinationPanWasDeactive = 4,
        [Description("تاریخ انقضا کارت نامعتبر است")]
        ExpDateWasExceded = 5,
        [Description("بانک مبدا پاسخ نداد")]
        SourceBankNotResponded = 6,
        [Description("بانک مقصد پاسخ نداد")]
        DestinationBankNotResponded = 7,
        [Description("موجودی کارت کم است")]
        BalanceIsLow = 8,
        [Description("TimeOut")]
        Timeout = 9
    }
}
