using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UiTest.Service.Cell
{
    public class CellTimer
    {
        private readonly Stopwatch stopwatch;
        private readonly List<Action<TimeSpan>> timeTicks;
        private System.Timers.Timer timer;
        public CellTimer()
        {
            stopwatch = new Stopwatch();
            timeTicks = new List<Action<TimeSpan>>();
            timer = new System.Timers.Timer()
            {
                Interval = 1000,
                AutoReset = true
            };
            timer.Elapsed += (s, e) =>
            {
                CallBack();
            };
        }

        private void CallBack()
        {
            if (!stopwatch.IsRunning) return;
            foreach (var item in timeTicks)
            {
                item.Invoke(stopwatch.Elapsed);
            }
        }

        public string StringTestTime => $"{stopwatch.Elapsed:dd\\.hh\\:mm\\:ss}";

        public long TestTime => (long) stopwatch.Elapsed.TotalMilliseconds;

        public void AddTimeTick(Action<TimeSpan> action)
        {
            if (action == null) return;
            timeTicks.Add(action);
        }

        public void Start()
        {
            timer.Start();
            stopwatch.Start();
        }

        public void Stop() 
        {
            timer.Stop();
            stopwatch.Stop();
            CallBack();
        }
    }
}
