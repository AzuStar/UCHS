using System;
using NoxRaven.Events.EventTypes;
namespace NoxRaven
{
    public partial class NoxUnit
    {
        public enum UnitTeam : ushort
        {
            Player = 0,
            Enemy = 1
        }

        public UnitTeam Team = UnitTeam.Player;
        public float Health;
        public float MaxHealth;
        public float SpellAmp;
        public float WeaponDamage;
        public float Mana;
        public float MaxMana;
        public float Armour;
        public float AttackReloadSpeed;
    }
}