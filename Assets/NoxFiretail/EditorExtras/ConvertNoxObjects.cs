using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// TODO: Make this object replace statically placed objects 
//[InitializeOnLoad]
//public class ConvertNoxObjects
//{
//    public const string MenuName = "Tools/Parse preplaced NoxObjects";
//    private static bool isToggled;

//    static ConvertNoxObjects()
//    {
//        EditorApplication.delayCall += () =>
//        {
//            isToggled = EditorPrefs.GetBool(MenuName, false);
//            UnityEditor.Menu.SetChecked(MenuName, isToggled);
//            SetMode();
//        };
//    }
    
//    [MenuItem(MenuName)]
//    private static void ToggleMode()
//    {
//        isToggled = !isToggled;
//        UnityEditor.Menu.SetChecked(MenuName, isToggled);
//        EditorPrefs.SetBool(MenuName, isToggled);
//        SetMode();
//    }

//    private static void SetMode()
//    {
//        if (isToggled)
//        {
//            EditorApplication.playModeStateChanged += ReplacePreplacedPrefabs;
//        }
//        else
//        {
//            EditorApplication.playModeStateChanged -= ReplacePreplacedPrefabs;
//        }
//    }

//    private static void ReplacePreplacedPrefabs(PlayModeStateChange state)
//    {
//        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
//        {
//            //foreach(GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
//            //    if()
//            //Debug.Log();

//        }
//    }
//}
