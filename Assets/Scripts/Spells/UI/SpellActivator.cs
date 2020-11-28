using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UCHS.Assets.Scripts.Spells.UI
{
    public class SpellActivator : MonoBehaviour
    {
        public Button ActivatorButton;
        public string Tooltip;



        // Start is called before the first frame update
        void Start()
        {
            ActivatorButton.onClick.AddListener(() => {
                SpellManager.TargetSpell(Tooltip);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}