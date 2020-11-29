using System;
using System.Collections;
using System.Collections.Generic;
using UCHS.Assets.Scripts.Spells.Mechanics;
using UCHS.Assets.Scripts.Spells.Spell_Definitions;
using UnityEngine;
using UnityEngine.UI;

using static NoxFiretail.Scripts.Core.GameCommon;

namespace UCHS.Assets.Scripts.Spells.UI
{
    public class SpellActivator : MonoBehaviour
    {
        public Button ActivatorButton;
        public UnityEngine.KeyCode ActivationKey;
        public Spell<LunarStarfallParams> ActiveSpell = GameGlobals._Self.sf.GenerateSpell(1);
        public bool OnCD = false;


        public void ActivateTargeting()
        {
            ActivatorButton.interactable = false;
            OnCD = true;
            SpellManager.TargetSpell(ActiveSpell.GetDescription(), this);
        }

        public void EnableSpell()
        {
            ActivatorButton.interactable = true;
            OnCD = false;
        }



        // Start is called before the first frame update
        void Start()
        {
            ActivatorButton.onClick.AddListener(ActivateTargeting);
        }

        // Update is called once per frame
        void Update()
        {
            if (!OnCD)
                if (Input.GetKeyDown(ActivationKey))
                    ActivateTargeting();
        }
    }
}