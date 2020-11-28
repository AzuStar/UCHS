using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static NoxFiretail.Scripts.Core.GameCommon;

namespace NoxFiretail.Scripts.Core
{
    public partial class GameManager
    {

        private static object TimerLock = new object();
        private static List<Timer> AddTimers = new List<Timer>();
        private static List<Timer> RunningTimers = new List<Timer>();
        private static List<Timer> RemoveTimers = new List<Timer>();

        partial void TimerCode()
        {
            lock (TimerLock)
            {
                if (AddTimers.Count > 0)
                {
                    RunningTimers.AddRange(AddTimers);
                    AddTimers.Clear();
                }
                if (RemoveTimers.Count > 0)
                {
                    RunningTimers = RunningTimers.Except(RemoveTimers).ToList();
                    RemoveTimers.Clear();
                }
                foreach (Timer t in RunningTimers)
                {
                    if (!t.Paused)
                        if ((t.Remain -= Time.deltaTime) <= 0)
                        {
                            if (!t.Periodic) RemoveTimers.Add(t);
                            else t.Remain += t.Duration;
                            t.Callback.Invoke();
                        }
                }
            }
        }

        public void TimerStart(Timer t)
        {
            lock (TimerLock)
                AddTimers.Add(t);
        }

        public void TimerStop(Timer t)
        {
            lock (TimerLock)
                RemoveTimers.Add(t);
        }
    }
}