namespace Common
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.TotalSeconds}s, {message}");
        }
    }
}
