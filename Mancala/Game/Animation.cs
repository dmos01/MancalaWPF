using System;
using System.Windows.Threading;

namespace Mancala.Game
{
    public static class Animation
    {
        public static TimeSpan HumanTimeSpan;
        public static TimeSpan ComputerTimeSpan;
        public static TimeSpan ComputerTimeSpanTwice;
        public static bool HumanAnimationIsOn => HumanTimeSpan != TimeSpan.Zero;
        public static bool ComputerAnimationIsOn => ComputerTimeSpan != TimeSpan.Zero;
        

        static DispatcherTimer timer;

        public static void StartHumanTimer(EventHandler tick)
        {
            timer = new DispatcherTimer { Interval = HumanTimeSpan };
            timer.Tick += tick;
            timer.Start();
        }

        public static void StartComputerTimer(EventHandler tick)
        {
            timer = new DispatcherTimer { Interval = ComputerTimeSpan };
            timer.Tick += tick;
            timer.Start();
        }

        public static void StartTimerForComputerChosenCupOrNewLapCup(EventHandler tick)
        {
            timer = new DispatcherTimer { Interval = ComputerTimeSpanTwice };
            timer.Tick += tick;
            timer.Start();
        }
    }
}