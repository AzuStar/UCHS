using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static NoxFiretail.Scripts.Core.GameCommon;

namespace NoxRaven
{
    public sealed class OnHit
    {
        public dynamic Data;
        private OnHitType Type;
        public int Count = 0;

        internal OnHit(OnHitType type)
        {
            Type = type;
        }
        /// <summary>
        /// Reverse the source and target to make it AmHit.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void ApplyOnHit(NoxUnit source, NoxUnit target, float damage, float processedDamage)
        {
            if (Random.value < source.TriggerChance * Type.Chance)
                if (!Type.Epic)
                    for (int i = 0; i < Count; i++)
                        Type.Callback.Invoke(source, target, damage, processedDamage, this);
                else Type.Callback.Invoke(source, target, damage, processedDamage, this);
        }
    }
}
