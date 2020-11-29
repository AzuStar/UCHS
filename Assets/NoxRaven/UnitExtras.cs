using System;
using System.Collections.Generic;
using System.Text;

using static NoxFiretail.Scripts.Core.GameCommon;

namespace NoxRaven
{
    /// <summary>
    /// Some really special utilities that do not really belong (or may be not yet) in NoxUnit
    /// </summary>
    // public static class UnitExperimentals
    // {
    //     static Timer Knockback;
    //     static List<PushedUnit> Pushed = new List<PushedUnit>();
    //     static UnitExperimentals()
    //     {
    //         Knockback = new Timer(30, true,() =>
    //         {
    //             if (Pushed.Count > 0)
    //                 foreach (PushedUnit pu in Pushed)
    //                     if (pu.Move())
    //                         Pushed.Remove(pu);
    //         });
    //         // Max possible smoothness @ 64 fps
    //         Knockback.Start();
    //     }
    //     /// <summary>
    //     /// Pulls targets from range over duration with power stress. <br />
    //     /// Power - how hard this algortihm tries to pull targets. power = 1 means everyone in range will gets slowly pulled towards center.
    //     /// </summary>
    //     /// <param name="range"></param>
    //     /// <param name="duration"></param>
    //     /// <param name="power"></param>
    //     //public static void PullTargets(float range, float duration, float power)
    //     //{

    //     //}

    //     public static void PushTarget(NoxUnit target, float duration, float angle, float range, bool actionBlocked)
    //     {
    //         Pushed.Add(new PushedUnit(target, duration, angle, range, actionBlocked));
    //     }

    // }

    // public class PushedUnit
    // {
    //     unit Unit;
    //     public float TimeLeft;
    //     public float Sin;
    //     public float Cos;
    //     public bool ActionsBlocked;

    //     public PushedUnit(unit Unit, float duration, float angle, float range, bool actionBlocked)
    //     {
    //         this.Unit = Unit;
    //         TimeLeft = duration;
    //         float push = range / duration * 30;
    //         Sin = push * Sin(angle * bj_DEGTORAD);
    //         Cos = push * Cos(angle * bj_DEGTORAD);
    //         ActionsBlocked = actionBlocked;
    //     }

    //     public bool Move()
    //     {
    //         float xi = GetUnitX(Unit) + Cos;
    //         float yi = GetUnitY(Unit) + Sin;
    //         if (Utils.IsCurrentlyWalkable(xi, yi))
    //             if (ActionsBlocked)
    //             {
    //                 SetUnitPosition(Unit, xi, yi);
    //             }
    //             else
    //             {
    //                 SetUnitX(Unit, xi);
    //                 SetUnitY(Unit, yi);
    //             }
    //         return (TimeLeft -= Master.FPS_TIME) <= 0;
    //     }
    // }
}
