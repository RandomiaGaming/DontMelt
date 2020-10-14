using System;
using System.Diagnostics;
namespace DontMelt.Timers.Default
{
    public static class DefaultTimer
    {
        private static Stopwatch gameTimer = new Stopwatch();
        public static TimeSpan totalDeltaTime = new TimeSpan(0);
        public static TimeSpan frameDeltaTime = new TimeSpan(0);
        private static long totalDeltaTimeLastFrame = 0;
        public static void Initialize()
        {
            gameTimer = new Stopwatch();
            totalDeltaTime = new TimeSpan(0);
            frameDeltaTime = new TimeSpan(0);
            totalDeltaTimeLastFrame = 0;
            gameTimer.Start();
        }
        internal static void Update()
        {
            totalDeltaTime = new TimeSpan(gameTimer.ElapsedTicks);
            frameDeltaTime = new TimeSpan(gameTimer.ElapsedTicks - totalDeltaTimeLastFrame);
            totalDeltaTimeLastFrame = gameTimer.ElapsedTicks;
        }
    }
}