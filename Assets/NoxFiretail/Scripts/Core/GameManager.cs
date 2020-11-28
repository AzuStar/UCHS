using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NoxFiretail.Scripts.Core
{
    /// <summary>
    /// <b>This class does not exist for you!<br></br> You should never touch anything in this class.</b>
    /// </summary>
    [ScriptExecOrder(-1)]
    public sealed partial class GameManager : MonoBehaviour
    {
        #region System Access
        public static GameManager _SelfRef { get; private set; } = null;
        #endregion

        //Custom game main instance loop
        private void Awake()
        {
            QualitySettings.vSyncCount = 1;
            if (_SelfRef == null)
                _SelfRef = transform.GetComponent<GameManager>();
            // export self as the api
            GameCommon.IGC = this;
            // TextAsset text = Resources.Load<TextAsset>("NoxFiretail/ObjectsMeta");
            // if (text != null)
            // {
            //     List<NoxUnityObjectMeta> MetaList = JsonConvert.DeserializeObject<List<NoxUnityObjectMeta>>(text.ToString());
            //     foreach (NoxUnityObjectMeta nom in MetaList)
            //         //nom._Type = GetType(nom.Type);
            //         MetaStore.Add(FiveCC(nom.MetaStringID), nom);
            // }
            // else
            // {
            //     ForceQuit("Could not load/locate/deserialize Object Meta Data at Assets/Resources/NoxFiretail/ObjectsMeta.json");
            // }
            if (GameCommon.FirestEverRun = File.Exists(Application.persistentDataPath + "/main.bin"))
                MainDataIndexCall();
            else
            MainDataIndexCall();
                MainDataIndex = new StoreIndex();
        }
        //[MethodImpl(MethodImplOptions.NoInlining)] // if {if()} else{} can inline into if(if1 && if2) else, which is not allowed
        // reversed logic, now it's fine
        private static Type GetType(string typeName)
        {
            List<Type> matched = new List<Type>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                if (matched.Count > 1)
                {
                    string message = "Multiple matches found:\n\r";
                    foreach (Type ti in matched)
                        message += ti.FullName + "\n\r";
                    ForceQuit(message);
                }
                else
                {
                    if (t.Name == typeName)
                        matched.Add(t);
                }
            if (matched.Count == 1) return matched[0];
            ForceQuit("Type " + typeName + " misspelled or doesn't exist!");
            throw new Exception("haha mem kekw"); // unreachable code, but for c# compiler
        }

        public static void ForceQuit(string message)
        {
#if DEBUG
            Debug.LogError(message);
            Application.Quit();
#endif
        }

        private void Start()
        {

        }
    }
}