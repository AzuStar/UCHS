using NoxRaven;
using NoxRaven.Statuses;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static NoxFiretail.Scripts.Core.GameCommon;
using static NoxRaven.NoxUnit;

namespace UCHS.Assets.Scripts.Spells.UI
{

    public class SpellManager : MonoBehaviour
    {

        public static PeriodicStatusType Poison = new PeriodicStatusType((status) =>
        {
            status.Source.DealPhysicalDamage(status.Target, 10, false, false, true, false);
        }, (status) =>
        {
        }, (status) =>
        {
        }, 0.25f);

        public static SpellManager _Self;

        public Button Exit;
        public Text Tooltip;
        public GameObject ActivatorPane;
        public SpellActivator ActivatedSpell;

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
            _Self.ActivatorPane.SetActive(false);
            _Self.CommandButtonsPalette.SetActive(true);
            GameGlobals._Self.PlayerController.MovementAllowed = true;
            _Self.ActivatedSpell = null;
        }

        public static void TargetSpell(string tooltip, SpellActivator activator)
        {
            _Self.Tooltip.text = tooltip;
            _Self.ActivatorPane.SetActive(true);
            _Self.CommandButtonsPalette.SetActive(false);
            GameGlobals._Self.PlayerController.MovementAllowed = false;
            _Self.ActivatedSpell = activator;
        }

        public void Update()
        {
            if (ActivatedSpell!=null)
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                        if (hit.collider != null)
                            if (hit.collider.GetComponent<NoxUnit>() != null)
                                if (hit.collider.GetComponent<NoxUnit>().Team == UnitTeam.Enemy)
                                {
                                    NoxUnit hitEnemy = hit.collider.GetComponent<NoxUnit>();
                                    NoxUnit player = GameGlobals._Self.PlayerController.GetComponent<NoxUnit>();
                                    // don't you dare click multiple times
                                    CancelTargeting();
                                    player.GetComponent<Animator>().SetBool("CastSpell", true);
                                    // Animation
                                    Timer tim = new Timer(1.1f, false, () =>
                                    {
                                        ActivatedSpell.ActiveSpell.LaunchSpell(player, hitEnemy);
                                        player.GetComponent<Animator>().SetBool("CastSpell", false);
                                    });
                                    tim.Start();
                                }
                }

        }
    }
}