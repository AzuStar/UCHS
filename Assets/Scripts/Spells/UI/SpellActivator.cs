using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static NoxFiretail.Scripts.Core.GameCommon;

namespace UCHS.Assets.Scripts.Spells.UI
{
    public class SpellActivator : MonoBehaviour
    {
        public Button ActivatorButton;
        public string Tooltip;
        public UnityEngine.KeyCode ActivationKey;


        public void PutOnCooldown()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            ActivatorButton.onClick.AddListener(() =>
            {
                SpellManager.TargetSpell(Tooltip);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}