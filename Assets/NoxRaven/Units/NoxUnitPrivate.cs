using System;
using System.Collections.Generic;
using NoxRaven.Statuses;
using NoxRaven;
using NoxRaven.Events.EventTypes;
using UnityEngine;

using static NoxFiretail.Scripts.Core.GameCommon;

namespace NoxRaven
{
    [Serializable]
    public partial class NoxUnit
    {
        public static Dictionary<int, NoxUnit> Indexer = new Dictionary<int, NoxUnit>();
        // private static float KeepCorpsesFor = 25;
        public const float RegenerationTimeout = 0.04f;
        /// <summary>
        /// Default game reduction constant.
        /// </summary>
        public const float ARMOR_CONST = 0.06f;

        void Start()
        {
            Health = MaxHealth;
            CanvasController.OnHealthChanged(Health, MaxHealth);
            if (Indexer.ContainsKey(gameObject.GetInstanceID())) return;
            Indexer[gameObject.GetInstanceID()] = this;
            return;
        }

        private void Remove(NoxUnit killer)
        {
            KillEvent @event = new KillEvent()
            {
                EventInfo = new NoxRaven.Events.Metas.KillMeta()
                {
                    Killer = killer,
                    Dying = this
                }
            };
            killer.OnKill(@event);
            OnDeath(@event);

            foreach (Status st in Statuses.Values)
                st.Remove();
            Statuses.Clear();// just in case

            OnRemoval(new RemovalEvent() { Target = this });
            OnHits.Clear();
            Indexer.Remove(gameObject.GetInstanceID());
            Destroy(gameObject);
        }

        // /// <summary>
        // /// Set unit's -xxx.x or +xxx.x armor. Does support single precision decimal point.
        // /// </summary>
        // /// <param name="val"></param>
        // protected void SetGreenArmor(float val)
        // {
        //     int leftover = R2I(val);
        //     int decimals = R2I((val - R2I(val)) * 10);
        //     if (val < 0) decimals = 1 - decimals;
        //     SetUnitAbilityLevel(_Self, FourCC("ARDP"), decimals + 1);
        //     GreenArmor = val;
        //     foreach (int abil in Abilities_BonusArmor)
        //         UnitRemoveAbility(this, abil);
        //     foreach (int abil in Abilities_Corruption)
        //         UnitRemoveAbility(this, abil);

        //     if (leftover < 0)
        //     {
        //         leftover = -leftover;
        //         for (int i = Abilities_Corruption.Length - 1; i >= 0; i--)
        //         {
        //             int comparator = R2I(Pow(2, i));
        //             if (comparator <= leftover)
        //             {
        //                 UnitAddAbility(this, Abilities_Corruption[i]);
        //                 leftover -= comparator;
        //             }
        //         }
        //     }
        //     else
        //         for (int i = Abilities_BonusArmor.Length - 1; i >= 0; i--)
        //         {
        //             int comparator = R2I(Pow(2, i));
        //             if (comparator <= leftover)
        //             {
        //                 UnitAddAbility(this, Abilities_BonusArmor[i]);
        //                 leftover -= comparator;
        //             }
        //         }
        // }

        // // This guy makes +xxx damage magic
        // protected void SetGreenDamage(int val)
        // {
        //     GreenDamage = val;
        //     for (int i = Abilities_BonusDamage.Length - 1; i >= 0; i--)
        //     {
        //         UnitRemoveAbility(this, Abilities_BonusDamage[i]);
        //         int comparator = R2I(Pow(2, i));
        //         if (comparator <= val)
        //         {
        //             UnitAddAbility(this, Abilities_BonusDamage[i]);
        //             val -= comparator;
        //         }
        //     }
        // }

        protected void CalculateTotalHP()
        {
            CalculateTotalEvent ev = new CalculateTotalEvent()
            {
                ExpectedTotal = BaseHP + TotalHPPercent,
            };
            OnCalculateTotalHP(ev);
            MaxHealth = ev.ExpectedTotal;
        }

        protected void CalculateTotalMana()
        {
            CalculateTotalEvent ev = new CalculateTotalEvent()
            {
                ExpectedTotal = BaseHP + TotalHPPercent,
            };
            OnCalculateTotalMana(ev);
            MaxMana = ev.ExpectedTotal;
        }

        public virtual void Regenerate()
        {
            RegenerationTickEvent parsEvent = new RegenerationTickEvent()
            {
                EventInfo = new NoxRaven.Events.Metas.RegenerationMeta()
                {
                    Target = this
                },
                HealthValue = RegenFlat,
                ManaValue = RegenManaFlat
            };
            Heal(parsEvent.HealthValue * RegenerationTimeout);
            ReplenishMana(parsEvent.ManaValue * RegenerationTimeout);
        }

        #region internal Status api
        internal bool ContainsOnHit(int id)
        {
            return OnHits.ContainsKey(id);
        }
        internal OnHit AddOnHit(int id, OnHit toAdd)
        {
            OnHits.Add(id, toAdd);
            return toAdd;
        }
        internal OnHit GetOnHit(int id)
        {
            return OnHits[id];
        }
        internal void RemoveOnHit(int id)
        {
            OnHits.Remove(id);
        }
        internal bool ContainsStatus(int id)
        {
            //if(st.GetHashCode()<=100)
            return Statuses.ContainsKey(id);
        }
        internal Status AddStatus(int id, Status toAdd)
        {
            Statuses.Add(id, toAdd);
            return toAdd;
        }
        #endregion

        // may be implicit operator, Ive been down this road
        public static NoxUnit Cast(GameObject u)
        {
            return u.GetComponent<NoxUnit>();
        }
        public static implicit operator NoxUnit(GameObject u)
        {
            return Cast(u);
        }
    }
}