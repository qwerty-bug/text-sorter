using System.Diagnostics;

namespace Common
{
    public static class GlobalTimer
    {
        public static Stopwatch StopWatch { get; private set; }

        static GlobalTimer()
        {
            StopWatch = new Stopwatch();
        }

        public static void StartNew() {
            StopWatch = new Stopwatch();
            StopWatch.Start();
        }
    }
}
