using static NoxFiretail.Scripts.Core.GameCommon;
using NoxRaven;
using System;
using UnityEngine;

namespace UCHS.Assets.Scripts.Spells.Mechanics
{
    public abstract class SpellParams { }
    public abstract class SingleTargetSpellDefinition<T> where T : SpellParams
    {
        public delegate void SpellAction(NoxUnit source, NoxUnit target, int level, T param);
        public abstract float CD { get; }
        public abstract SpellAction Callback { get; }
        // Player-only
        public abstract string PreparedDescription { get; }
        public abstract string IconResourcePath { get; }

        public Spell<T> GenerateSpell(int level)
        {
            return new Spell<T>(this, level);
        }

    }
}