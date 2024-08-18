[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
namespace Common
{
    public static class Logger
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static void Log(string message)
        {
            logger.Info($"[{GlobalTimer.StopWatch.Elapsed.TotalSeconds:0.000}s] {message}");
        }

        public static void LogError(string message)
        {
            logger.Error($"[{GlobalTimer.StopWatch.Elapsed.TotalSeconds:0.000}s] {message}");
        }
    }
}
