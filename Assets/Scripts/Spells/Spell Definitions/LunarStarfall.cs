using System.Diagnostics;
using UCHS.Assets.Scripts.Spells.Mechanics;

namespace UCHS.Assets.Scripts.Spells.Spell_Definitions
{
    public class LunarStarfallParams : SpellParams
    {
        public int BaseDamage;
        public int DamagePerLevel;
    }
    public class LunarStarfall : SingleTargetSpellDefinition<LunarStarfallParams>
    {

        public static LunarStarfallParams DefaultParams = new LunarStarfallParams(){
            BaseDamage = 125,
            DamagePerLevel = 25
        };

        public override float CD => 12;

        public override SpellAction Callback => (source, target, level, param) => { source.DealPhysicalDamage(target, param.BaseDamage + param.DamagePerLevel, false, true, true, false); };

        public override string PreparedDescription => "Deals {0} damage to enemy unit, this increases by {0} for each level of this ability.";

        public override string IconResourcePath => "Icons/SpellBookMegapack/SpellBook01_07";

        public override LunarStarfallParams Params => DefaultParams;
    }
}