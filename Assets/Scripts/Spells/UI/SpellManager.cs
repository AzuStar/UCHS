using UnityEditor.PackageManager;
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
        public bool SelectingTarget = false;

        public GameObject CommandButtonsPalette;
        void Awake()
        {
            _Self = this;
        }
        public void Start()
        {
            Exit.onClick.AddListener(CancelTargeting);
        }

        public static void CancelTargeting()
        {
            _Self.Activator.SetActive(false);
            _Self.CommandButtonsPalette.SetActive(true);
            GlobalVars._Self.PC.MovementAllowed = true;
            _Self.SelectingTarget = false;
        }

        public static void TargetSpell(string tooltip)
        {
            Debug.Log("targetting ");
            _Self.Tooltip.text = tooltip;
            _Self.Activator.SetActive(true);
            _Self.CommandButtonsPalette.SetActive(false);
            GlobalVars._Self.PC.MovementAllowed = false;
            _Self.SelectingTarget = true;
        }

        public void Update()
        {
            if (SelectingTarget)
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null)
                        {
                            Debug.Log(hit.collider.name);
                            if (hit.collider.GetComponent<Unit>() != null)
                                if (hit.collider.GetComponent<Unit>().Team == UnitTeam.Enemy)
                                {
                                    GlobalVars._Self.PC.GetComponent<Unit>().DealDamage(hit.collider.GetComponent<Unit>(), 125);
                                    CancelTargeting();
                                }
                        }
                    }
                }
        }
    }
}