using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;
using System.Linq;

using static NoxFiretail.Scripts.Core.GameCommon;
using UnityEngine;

namespace NoxRaven
{
    public static class Utils
    {
        /// <summary>
        /// Use this function to invoke something (anything) with a delay.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="effect"></param>
        public static void DelayedInvoke(float timeout, Action effect)
        {
            Timer t = new Timer(timeout, false, effect );
            t.Start();
        }

        public static float DistanceBetweenPoints(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        public static float AngleBetweenPoints(float x1, float y1, float x2, float y2)
        {
            return Mathf.Atan2(y2 - y1, x2 - x1) * Mathf.Rad2Deg + 180;
        }


    }
}
