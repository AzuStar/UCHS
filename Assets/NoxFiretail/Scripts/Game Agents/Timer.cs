using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoxFiretail.Scripts.Core
{
    public partial class GameCommon
    {

        public sealed class Timer
        {
            public bool Running { get; private set; } = false;
            public bool Paused { get; private set; } = true;
            public float Remain;
            public float Duration { get; private set; }
            public Action Callback { get; private set; }
            public bool Periodic { get; private set; }
            public Timer(float duration, bool periodic, Action callback)
            {
                Remain = Duration = duration;
                Periodic = periodic;
                Callback = callback;
            }

            public void Start()
            {
                Running = true;
                Paused = false;
                GameManager._SelfRef.TimerStart(this);
            }

            public float TimeElapsed()
            {
                return Duration - Remain;
            }

            public float TimeRemain()
            {
                return Remain;
            }

            public float TimeExpiration()
            {
                return Duration;
            }

            public void Pause()
            {
                Paused = true;
            }

            public void Continue()
            {
                Paused = false;
            }

            public void Stop()
            {
                Running = false;
                Paused = true;
                GameManager._SelfRef.TimerStop(this);
            }

        }
    }
}