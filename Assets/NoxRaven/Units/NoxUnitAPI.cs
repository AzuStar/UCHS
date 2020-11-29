using NoxRaven;
using NoxRaven.Events.EventTypes;
using NoxRaven.Events.Metas;
using System.Collections.Generic;
using System;
using NoxRaven.Statuses;
using UnityEngine;

namespace NoxRaven
{
    public partial class NoxUnit
    {

        public void Kill(NoxUnit whoToKill)
        {
            whoToKill.Remove(this);
        }


        /// <summary>
        /// Ensure all damage goes through this.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public void Damage(NoxUnit target, float damage)
        {
            target.Health -= damage;
            target.CanvasController.OnHealthChanged(target.Health, target.MaxHealth);
            if (target.Health <= 0 && target != null) target.Remove(this);
        }

        /// <summary>
        /// Damage parsers that takes care of all calculations. Damage parser calculates outgoing damage from the unit.
        /// </summary>
        /// <param name="target">Whos is the target</param>
        /// <param name="damage"></param>
        /// <param name="triggerOnHit">Does it apply on-hit effects?</param>
        /// <param name="triggerCrit">Can it crit?</param>
        public void DealPhysicalDamage(NoxUnit target, float damage, bool triggerOnHit, bool triggerCrit, bool isSpell, bool isRanged)
        {
            if (damage < 0) return;
            // location loc = Location(GetUnitX(target) + GetRandomReal(0, 5), GetUnitY(target) + GetRandomReal(0, 5));
            float pars = damage;
            float critC = CritChance;
            float critD = CritDamage;
            // maths
            pars *= (1 - Math.Min(target.DamageReduction, 1));
            float armor = target.Armour;
            if (armor < 0)
                pars *= (1.71f - Mathf.Pow(1f - ARMOR_CONST, -armor));
            else pars *= 1 / (1 + armor * ARMOR_CONST * (1 - ArmorPenetration)); // Inverse armor reduction function, got by solving: Armor * CONST / (1 + ARMOR * CONST)

            //Event Pars
            DamageEvent parsThroughUnit = new DamageEvent()
            {
                EventInfo = new DamageMeta()
                {
                    Source = this,
                    Target = target,
                    Damage = damage,
                    TriggerOnHit = triggerOnHit,
                    TriggerCrit = triggerCrit,
                    IsSpell = isSpell,
                    IsRanged = isRanged
                },

                ProcessedDamage = pars,
                CritChance = critC,
                CritDamage = critD
            };
            OnDealPhysicalDamage.Invoke(parsThroughUnit);
            target.OnRecievePhysicalDamage.Invoke(parsThroughUnit);
            pars = parsThroughUnit.ProcessedDamage;
            critC = parsThroughUnit.CritChance;
            critD = parsThroughUnit.CritDamage;
            // Display

            if (triggerCrit && UnityEngine.Random.value < critC)
            {
                pars *= critD;

                // Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 8.5f, 255, 0, 0, 0, 1.3f, GetOwningPlayer(this));
                // Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 8.5f, 255, 0, 0, 0, 1.3f, GetOwningPlayer(target));
            }
            else
            {
                // Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 6.9f, 255, 0, 0, 0, 0.8f, GetOwningPlayer(this));
                // Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 6.9f, 255, 0, 0, 0, 0.8f, GetOwningPlayer(target));
            }

            Damage(target, pars);

            if (isSpell) Heal(pars * SpellVamp);
            else Heal(pars * Lifesteal);
            // Now that's done
            // Onhits
            if (triggerOnHit)
            {
                List<OnHit> onhits = new List<OnHit>(OnHits.Values);
                foreach (OnHit onhit in onhits)
                    onhit.ApplyOnHit(this, target, damage, pars);
                //ApplyAmHits(source);
            }
        }
        // /// <summary>
        // /// Damage parsers that takes care of all calculations. Damage parser calculates outgoing damage from the unit.
        // /// </summary>
        // /// <param name="target">Whos is the target</param>
        // /// <param name="damage"></param>
        // /// <param name="triggerOnHit">Does it apply on-hit effects?</param>
        // /// <param name="triggerCrit">Can it crit?</param>
        // private void DealMagicalDamage(NoxUnit target, float damage, bool triggerOnHit, bool triggerCrit, bool isSpell, bool isRanged)
        // {
        //     location loc = Location(GetUnitX(target) + GetRandomReal(0, 5), GetUnitY(target) + GetRandomReal(0, 5));
        //     float pars = damage;
        //     float critC = CritChance;
        //     float critD = CritDamage;
        //     // maths
        //     pars *= (1 - Math.Min(target.DamageReduction, 1));
        //     // float armor = BlzGetUnitArmor(target);
        //     // if (armor < 0)
        //     //     pars *= (1.71f - Pow(1f - ARMOR_CONST, -armor)); // war3 real armor reduction is 1.71-pow(xxx) - why? - no idea
        //     // else pars *= 1 / (1 + armor * ARMOR_CONST * (1 - ArmorPenetration)); // Inverse armor reduction function, got by solving: Armor * CONST / (1 + ARMOR * CONST)

        //     //Event Pars
        //     DamageEvent parsThroughUnit = new DamageEvent()
        //     {
        //         EventInfo = new DamageMeta()
        //         {
        //             Source = this,
        //             Target = target,
        //             Damage = damage,
        //             TriggerOnHit = triggerOnHit,
        //             TriggerCrit = triggerCrit,
        //             IsSpell = isSpell,
        //             IsRanged = isRanged
        //         },

        //         ProcessedDamage = pars,
        //         CritChance = critC,
        //         CritDamage = critD
        //     };
        //     target.OnRecievePhysicalDamage.Invoke(parsThroughUnit);
        //     pars = parsThroughUnit.ProcessedDamage;
        //     critC = parsThroughUnit.CritChance;
        //     critD = parsThroughUnit.CritDamage;
        //     // The logic

        //     if (triggerCrit && GetRandomReal(0, 1) < critC)
        //     {
        //         pars *= critD;

        //         Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 8.5f, 128, 128, 255, 0, 1.3f, GetOwningPlayer(this));
        //         Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 8.5f, 128, 128, 255, 0, 1.3f, GetOwningPlayer(target));
        //     }
        //     else
        //     {
        //         Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 6.9f, 128, 128, 255, 0, 0.8f, GetOwningPlayer(this));
        //         Utils.TextDirectionRandom(Utils.NotateNumber(R2I(pars)), loc, 6.9f, 128, 128, 255, 0, 0.8f, GetOwningPlayer(target));
        //     }

        //     Damage(target, pars);

        //     if (isSpell) Heal(pars * SpellVamp);
        //     else Heal(pars * Lifesteal);
        //     // Now that's done
        //     // Onhits
        //     if (triggerOnHit)
        //     {
        //         List<OnHit> onhits = new List<OnHit>(OnHits.Values);
        //         foreach (OnHit onhit in onhits)
        //             onhit.ApplyOnHit(this, target, damage, pars);
        //         //ApplyAmHits(source);
        //     }

        //     // cleanup
        //     RemoveLocation(loc);
        //     loc = null;
        // }

        #region Mana
        public float GetBaseMana() => BaseMana;
        public void SetBaseMana(float val)
        {
            BaseMana = val;
            CalculateTotalMana();
        }
        public void AddBaseMana(float val)
        {
            BaseMana += val;
            CalculateTotalMana();
        }
        public float GetBonusManaPercent() => TotalManaPercent;
        public void SetBonusManaPercent(float percent)
        {
            TotalHPPercent = percent;
            CalculateTotalMana();
        }
        #endregion
        #region Health
        public float GetBaseHP() => BaseHP;
        public void SetBaseHP(float val)
        {
            BaseHP = val;
            CalculateTotalHP();
        }
        public void AddBaseHP(float val)
        {
            BaseHP += val;
            CalculateTotalHP();
        }
        public float GetBonusHPPercent() => TotalHPPercent;
        public void SetBonusHPPercent(float percent)
        {
            TotalHPPercent = percent;
            CalculateTotalHP();
        }
        #endregion

        public float GetAttackReload() => AttackReloadSpeed;
        public float GetAttackCooldown() => BaseAttackCooldown;
        /// <summary>
        /// Multiply base attack cooldown <br />
        /// 0.2 -> *1.2 <br />
        /// -0.2 -> /1.2
        /// </summary>
        /// <param name="val"></param>
        public void MultAttackCooldown(float val)
        {

            if (val < 0)
            {
                BaseAttackCooldown /= (1 - val);
            }
            else
            {
                BaseAttackCooldown *= (1 + val);
            }
            AttackReloadSpeed = BaseAttackCooldown / AttackSpeed;
        }
        public float GetAttackSpeed() => AttackSpeed;
        /// <summary>
        /// 5% = 0.05f
        /// </summary>
        /// <param name="attackSpeed"></param>
        public void AddAttackSpeed(float attackSpeed)
        {
            AttackSpeed += attackSpeed;
            AttackReloadSpeed = BaseAttackCooldown / AttackSpeed;
        }

        public float GetBaseDamage() => WeaponDamage;
        public int GetBonusDamage() => BonusDamage;
        public void AddBaseDamage(float val)
        {
            FloatEvent ev = new FloatEvent()
            {
                Value = val
            };
            OnAddBaseDamage(ev);
            WeaponDamage = ev.Value;
        }
        // public float GetBonusDamageMultiplier() => BonusDamagePercent;
        // public void AddBonusDamagePercent(float val)
        // {
        //     BonusDamagePercent += val;
        //     SetGreenDamage(R2I(BonusDamage + BlzGetUnitBaseDamage(_Self, 0) * BonusDamagePercent + Utils.ROUND_DOWN_CONST_OVERHEAD));
        // }
        // public void AddBonusDamageFlat(int val)
        // {
        //     BonusDamage += val;
        //     FloatEvent ev = new FloatEvent()
        //     {
        //         Value = BonusDamage
        //     };
        //     OnAddBonusDamage(ev);
        //     SetGreenDamage(R2I(ev.Value + BlzGetUnitBaseDamage(_Self, 0) * BonusDamagePercent + Utils.ROUND_DOWN_CONST_OVERHEAD));
        // }
        // /// <summary>
        // /// Get the +xxx on unit.
        // /// </summary>
        // /// <returns></returns>
        // public int GetGreenDamage() => GreenDamage;
        public float GetGreyArmor() => GreyArmor;
        public void AddGreyArmor(float val)
        {
            FloatEvent parsEvent = new FloatEvent()
            {
                Value = val
            };
            OnAddGreyArmor(parsEvent);
            GreyArmor += parsEvent.Value;
            Armour = val + GreenArmor;
        }
        // /// <summary>
        // /// Get units's -xxx armor.
        // /// </summary>
        // /// <returns></returns>
        // public float GetGreenArmor() => GreenArmor;
        // public void AddGreenArmor(float val)
        // {
        //     FloatEvent parseEvent = new FloatEvent()
        //     {
        //         Value = val
        //     };
        //     OnAddGreenArmor(parseEvent);
        //     GreenArmor += parseEvent.Value;
        //     SetGreenArmor(val);
        // }

        public void RemoveStatus(int id)
        {
            Statuses.Remove(id);
        }
        public Status GetStatus(int id)
        {
            return Statuses[id];
        }
        /// <summary>
        /// Heal unit by % missing hp. Unit with 50% HP and 10% Missing Healing will receive effective 5% of max hp heal.
        /// </summary>
        /// <param name="percentHealed"></param>
        public void HealPercentMissing(float percentHealed, bool show = false)
        {
            Heal(percentHealed * (MaxHealth - Health), show);
        }
        /// <summary>
        /// Simple function that heals a unit by percentHealed (%) of Max HP.<para />
        /// Range of percentHealed: 0.00 - 1.00
        /// </summary>
        /// <param name="percentHealed"></param>
        public void HealPercentMax(float percentHealed, bool show = false)
        {
            Heal(percentHealed * MaxHealth, show);
        }
        /// <summary>
        /// Simple function that heals a unit by howMuch amount (flat).
        /// </summary>
        /// <param name="howMuch"></param>
        public virtual void Heal(float howMuch, bool show = false)
        {
            float pars = howMuch * HealingFromAllSources;
            RegenerationEvent ev = new RegenerationEvent()
            {
                PredictedRegeneration = pars
            };
            OnRegenerateHP(ev);
            pars = ev.PredictedRegeneration;
            Health += pars;
            // if (show)
            // {
            //     location loc = Location(GetUnitX(this) + GetRandomReal(0, 10), GetUnitY(this) + GetRandomReal(0, 5));
            //     Utils.TextDirectionRandom("+" + Utils.NotateNumber(R2I(pars)), loc, 5.7f, 128, 255, 128, 0, 0.7f, GetOwningPlayer(this));
            //     RemoveLocation(loc);
            //     loc = null;
            // }
        }
        public virtual void ReplenishMana(float howMuch, bool show = false)
        {
            RegenerationEvent ev = new RegenerationEvent()
            {
                PredictedRegeneration = howMuch
            };
            OnRegenerateMana(ev);
            howMuch = ev.PredictedRegeneration;
            Mana += howMuch * ManaFromAllSources;
            // if (show)
            // {
            //     location loc = Location(GetUnitX(this) + GetRandomReal(0, 10), GetUnitY(this) + GetRandomReal(0, 5));
            //     Utils.TextDirectionRandom("+" + Utils.NotateNumber(R2I(howMuch)), loc, 5.7f, 128, 128, 255, 0, 0.7f, GetOwningPlayer(this));
            //     RemoveLocation(loc);
            //     loc = null;
            // }
        }
    }
}