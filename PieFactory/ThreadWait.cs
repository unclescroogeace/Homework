using System.Diagnostics;

namespace PieFactory
{
    public static class ThreadWait
    {
        public static void ThreadWaitMilisseconds(int milliseconds)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < milliseconds);
        }
    }
}