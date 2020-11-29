using UnityEngine;

namespace NoxRaven
{
    public partial class NoxUnit : MonoBehaviour
    {
        public void SetRunning(bool state)
        {
            Animator.SetBool("IsRunning", state);
        }

        public void SetCastSpell(bool state)
        {
            Animator.SetBool("CastSpell", state);
        }
    }
}