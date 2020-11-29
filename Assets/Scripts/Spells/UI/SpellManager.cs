using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static NoxFiretail.Scripts.Core.GameCommon;

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
            GameGlobals._Self.PlayerController.MovementAllowed = true;
            _Self.SelectingTarget = false;
        }

        public static void TargetSpell(string tooltip)
        {
            _Self.Tooltip.text = tooltip;
            _Self.Activator.SetActive(true);
            _Self.CommandButtonsPalette.SetActive(false);
            GameGlobals._Self.PlayerController.MovementAllowed = false;
            _Self.SelectingTarget = true;
        }

        public void Update()
        {
            if (SelectingTarget)
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                        if (hit.collider != null)
                            if (hit.collider.GetComponent<Unit>() != null)
                                if (hit.collider.GetComponent<Unit>().Team == UnitTeam.Enemy)
                                {
                                    GameGlobals._Self.PlayerController.PossessedUnit.Animator.SetBool("CastSpell", true);
                                    GameGlobals._Self.PlayerController.agent.isStopped = true;
                                    GameGlobals._Self.PlayerController.PossessedUnit.SetRunning(false);
                                    GameGlobals._Self.PlayerController.PossessedUnit.transform.LookAt(hit.transform);
                                    // Animation
                                    Timer tim = new Timer(1.1f, false, () =>
                                    {
                                        GameGlobals._Self.PlayerController.GetComponent<Unit>().DealDamage(hit.collider.GetComponent<Unit>(), 125);
                                        CancelTargeting();
                                        GameGlobals._Self.PlayerController.PossessedUnit.Animator.SetBool("CastSpell", false);
                                    });
                                    tim.Start();
                                }
                }
        }
    }
}