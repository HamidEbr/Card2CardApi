using Microsoft.Extensions.Logging;

namespace Card2CardApi.Service.Utility
{
    public static class LoggerExtension
    {
        public static void LogTrace(this ILogger logger, LogStruct logStruct)
        {
            logger.LogTrace("{@jsonMessage}", logStruct.ToJson());
        }

        public static void LogInformation(this ILogger logger, LogStruct logStruct)
        {
            logger.LogInformation("{@jsonMessage}", logStruct.ToJson());
        }

        public static void LogDebug(this ILogger logger, LogStruct logStruct)
        {
            logger.LogDebug("{@jsonMessage}", logStruct.ToJson());
        }

        public static void LogError(this ILogger logger, LogStruct logStruct)
        {
            logger.LogError("{@jsonMessage}", logStruct.ToJson());
        }

        public static void LogCritical(this ILogger logger, LogStruct logStruct)
        {
            logger.LogCritical("{@jsonMessage}", logStruct.ToJson());
        }

        public static void LogWarning(this ILogger logger, LogStruct logStruct)
        {
            logger.LogWarning("{@jsonMessage}", logStruct.ToJson());
        }
    }
}
