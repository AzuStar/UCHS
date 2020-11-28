using UnityEngine;

namespace UCHS.Assets.Scripts
{
    public class GlobalVars : MonoBehaviour
    {
        public static GlobalVars _Self;

        public PlayerController PC;

        public void Awake(){
            _Self = this;
        }
    }
}