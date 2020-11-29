using static NoxFiretail.Scripts.Core.GameCommon;
using NoxRaven;
using System;

namespace UCHS.Assets.Scripts.Spells.Mechanics
{

    

    public class SingleTargetSpell
    {
        public delegate void SpellAction(NoxUnit source, NoxUnit target);
        public float CD = 10;
        public SpellAction Callback;
        // Player-only
        public string PreparedDescription;

        public SingleTargetSpell(float cooldown, SpellAction callback, string description){
            CD = cooldown;
            Callback = callback;
            PreparedDescription = description;
        }

        public void LaunchSpell(NoxUnit source, NoxUnit target){
            if(source.Team == NoxUnit.UnitTeam.Player){

            }
        }
    }
}