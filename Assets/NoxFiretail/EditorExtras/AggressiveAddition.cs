using NoxFiretail.Scripts.Core;
using UnityEditor;
using UnityEngine;
// Adds required Nox Core
[InitializeOnLoad]
public class AggressiveAddition
{

    static AggressiveAddition()
    {
        EditorApplication.playModeStateChanged += (state) =>
        {
            bool flag = true;
            Object[] objs = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach (GameObject obj in objs)
            {
                if (obj.name == "NoxFiretailCore")
                {
                    Component[] components = obj.GetComponents<Component>();
                    foreach (Component comp in components)
                        if (comp == null)
                            GameObject.DestroyImmediate(comp);
                    if (obj.GetComponent<GameManager>() == null)
                        obj.AddComponent<GameManager>();
                    flag = false;
                    break;
                }
            }
            if (flag)
                new GameObject("NoxFiretailCore", typeof(GameManager));
        };
    }
}