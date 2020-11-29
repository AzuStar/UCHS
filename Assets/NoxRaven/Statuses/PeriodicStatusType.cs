using System;
using System.Collections.Generic;
using System.Text;

namespace NoxRaven.Statuses
{
    public class PeriodicStatusType : TimedStatusType
    {
        public float PeriodicTimeoutTime;
        // public bool PeriodicEffect;

        public PeriodicStatusType(StatusFunction apply, StatusFunction reset, StatusFunction onRemove, float periodicTimeoutTime = 1) : base(apply, reset, onRemove)
        {
            // PeriodicEffect = periodicEffect;
            PeriodicTimeoutTime = periodicTimeoutTime;
        }

        private new Status ApplyStatus(NoxUnit source, NoxUnit target, int level){
            return null;
        }

        public override Status ApplyStatus(NoxUnit source, NoxUnit target, int level, float duration)
        {
            if (!target.ContainsStatus(Id))
                // create new status and add it to unit
                return target.AddStatus(Id, new Status(Id, this, source, target, level, 0, 0, duration, PeriodicTimeoutTime, false, true, false));
            return target.GetStatus(Id).Reapply(duration, level, 0);
        }

    }
}
