using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Card2CardApi.Service.Utility
{
    public class LogStruct
    {
        public string Message { get; init; }
        public string ServiceName { get; init; }
        public object InputParams { get; init; }
        public object Results { get; init; }
        public Stopwatch ResponseTimeStopWatcher { get; init; }
        public Exception Exception { get; init; }
        public LogMessageTag Tags { get; set; }
        public string Method { get; set; }


        public object ToJson()
        {
            try
            {
                return JsonConvert.SerializeObject(new
                {
                    responseTime = Math.Round(ResponseTimeStopWatcher?.Elapsed.TotalMilliseconds ?? 0, MidpointRounding.AwayFromZero),
                    serviceName = ServiceName?.Trim(),
                    method = Method ?? "",
                    inputParams = JsonConvert.SerializeObject(InputParams),
                    results = Results is string ? Results : JsonConvert.SerializeObject(Results),
                    exception = JsonConvert.SerializeObject(Exception),
                    message = (string.IsNullOrEmpty(Message) && Exception != null) ? Exception.Message : Message,
                    tags = Tags.ToString()
                });
            }
            catch (Exception exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    message = $"An exception occurred in AsanPardakhtELKLogStruct serializer for {ServiceName}",
                    serviceName = nameof(LogStruct),
                    exception = JsonConvert.SerializeObject(exception)
                });
            }
        }
    }

    [Flags]
    public enum LogMessageTag
    {
        Input = 1,
        ExternalService = 2,
    }
}
