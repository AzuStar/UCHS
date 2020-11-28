using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace NoxFiretail.EditorExtras
{
    [InitializeOnLoad]
    public class EditorWindowFocusUtility
    {

        public static event Action OnUnityEditorFocusGain = () => { };
        public static event Action OnUnityEditorFocusLost = () => { };
        private static bool _appFocused;
        static EditorWindowFocusUtility()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (!_appFocused && UnityEditorInternal.InternalEditorUtility.isApplicationActive)
            {
                _appFocused = UnityEditorInternal.InternalEditorUtility.isApplicationActive;
                OnUnityEditorFocusGain();
            }
            else if (_appFocused && !UnityEditorInternal.InternalEditorUtility.isApplicationActive)
            {
                _appFocused = UnityEditorInternal.InternalEditorUtility.isApplicationActive;
                OnUnityEditorFocusLost();
            }
        }
    }
}
