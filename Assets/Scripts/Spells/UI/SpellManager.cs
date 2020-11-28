using UnityEngine;
using UnityEngine.UI;

namespace UCHS.Assets.Scripts.Spells.UI
{
    public class SpellManager : MonoBehaviour
    {
        public static SpellManager _Self;

        public Button Exit;
        public Text Tooltip;
        public GameObject Activator;

        public GameObject CommandButtonsPalette;
        void Awake()
        {
            _Self = this;
        }
        public void Start()
        {
            Exit.onClick.AddListener(() =>
            {
                Activator.SetActive(false);
                CommandButtonsPalette.SetActive(true);
            });
        }

        public static void TargetSpell(string tooltip)
        {
            _Self.Tooltip.text = tooltip;
            _Self.Activator.SetActive(true);
            _Self.CommandButtonsPalette.SetActive(false);
        }
    }
}