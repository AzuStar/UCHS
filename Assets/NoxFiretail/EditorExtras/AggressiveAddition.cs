using NoxFiretail.Scripts.Core;
using UnityEditor;
using UnityEngine;
// Adds required Nox Core
[InitializeOnLoad]
public class AggressiveAddition
{

    static AggressiveAddition()
    {
        bool flag = true;
        Object[] objs = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (GameObject obj in objs)
        {
            if (obj.name == "NoxFiretail Core")
            {
                if (obj.GetComponent<GameManager>() == null)
                    obj.AddComponent<GameManager>();
                flag = false;
                break;
            }
        }
        if (flag)
            new GameObject("NoxFiretail Core", typeof(GameManager));
    }
}