using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class OpenGameSave
{
    public const string MenuName = "Tools/Open Save Folder";

    [MenuItem(MenuName)]
    private static void OpenSaveFolder()
    {
        string dummy = Application.persistentDataPath + @"/Unity";
        Directory.CreateDirectory(dummy);
        EditorUtility.RevealInFinder(dummy);

    }

}
