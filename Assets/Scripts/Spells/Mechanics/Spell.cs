using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using NoxRaven;
using UCHS.Assets.Scripts.Spells.UI;
using UnityEngine;

using static NoxFiretail.Scripts.Core.GameCommon;

namespace UCHS.Assets.Scripts.Spells.Mechanics
{
    public class Spell<T> where T : SpellParams
    {
        public int Level { get; private set; }
        public SingleTargetSpellDefinition<T> Definition;
        public string Description;

        public Timer CDTimer;
        public SpellActivator ButtonActivator;

        public Spell(SingleTargetSpellDefinition<T> spellDefinition, int level)
        {
            Definition = spellDefinition;
            Level = level;
        }

        public void ChangeLevel(int lvl)
        {
            string tmp = Definition.PreparedDescription;
            // FieldInfo[] fis = typeof(T).GetFields();
            foreach (FieldInfo fi in typeof(T).GetFields())
            {
                Regex reg = new Regex(Regex.Escape("{0}"));
                reg.Replace(tmp, fi.GetValue(null).ToString(), 1);
            }
            Description = tmp;
        }

        public void LaunchSpell(NoxUnit source, NoxUnit target)
        {

            Definition.Callback.Invoke(source, target, Level, Definition.Params);
        }

        public string GetDescription()
        {
            return Description;
        }

        public Sprite GetSprite()
        {
            return Resources.Load<Sprite>(Definition.IconResourcePath);
        }
    }
}