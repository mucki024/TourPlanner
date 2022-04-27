namespace TourPlanner.Logging
{
    public static class LoggerFactory
    {
        public static ILoggerWrapper GetLogger()
        {
            return Log4NetWrapper.CreateLogger("../../../../Logging/log4net.config");
        }
    }
}
