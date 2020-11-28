using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

// [InitializeOnLoad]
// public class ResourceFolderCheck
// {
//     private const String ResourcesPath = @"/Resources/NoxFiretail/";
//     private static bool CreateFolders = true;
//     static ResourceFolderCheck()
//     {
//         if (!File.Exists(Application.dataPath + ResourcesPath + "ObjectsMeta.json"))
//         {
//             if (CreateFolders)
//                 if (CreateFolders = EditorUtility.DisplayDialog("ObjectsMeta.json not found", "Would you like to create examples in Assets/Resources folder?", "Create", "No"))
//                 {
//                     Debug.Log("Creating structure at " + Application.dataPath + ResourcesPath);
//                     Directory.CreateDirectory(Application.dataPath + ResourcesPath);
//                     File.Copy(Application.dataPath + "/NoxFiretail/Examples/Resources/NoxFiretail/ObjectsMeta.json.example", Application.dataPath + ResourcesPath + "ObjectsMeta.json");
//                     Directory.CreateDirectory(Application.dataPath + ResourcesPath+ "SerialPrefabs/");
//                 }

//         }
//     }
// }
