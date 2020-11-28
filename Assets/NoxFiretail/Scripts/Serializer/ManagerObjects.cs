using System.Numerics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NoxFiretail.Scripts.Serializer;
using System.Runtime.Serialization;

#pragma warning disable CS0612 // Type or member is obsolete
namespace NoxFiretail.Scripts.Core
{
    public partial class GameManager
    {
        //public static Dictionary<long, GameObject> 
        private static SerializableData ActiveDataRef;

        private static Dictionary<long, NoxUnityObjectMeta> MetaStore = new Dictionary<long, NoxUnityObjectMeta>();
        /// <summary>
        /// You only need it in-game.
        /// </summary>
        private static Dictionary<GameObject, NoxUnityObject> InverseObjectStore = new Dictionary<GameObject, NoxUnityObject>();

        private static void DeserializeStore(string path)
        {
            ActiveDataRef = JsonConvert.DeserializeObject<SerializableData>(File.ReadAllText(Application.persistentDataPath + path), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            // !!!!!!!!!!!
            // Do not change to Newtonsoft.JSON.OnDeserialize Attribute
            // !!!!!!!!!!!
            ActiveDataRef.PostDeserialzation();
        }
        #region Stuff for NoxObject
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static void AddObject(long objID, NoxObject obj)
        {
            ActiveDataRef.CustomStore.Add(objID, obj);
        }
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static NoxObject GetObject(long ID)
        {
            return ActiveDataRef.CustomStore[ID];
        }
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static NoxUnityObject GetNoxObject(GameObject obj)
        {
            return InverseObjectStore[obj];
        }
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static NoxUnityObject GetNoxObject(BigInteger UID)
        {
            return ActiveDataRef.ObjectStore[UID.ToString()];
        }
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static SerializableBigInt GetMyIUID(NoxUnityObject obj)
        {
            InverseObjectStore.Add(obj.Puppet, obj);
            ActiveDataRef.ObjectStore.Add(++ActiveDataRef.LifeTimeObjects, obj);
            return ActiveDataRef.LifeTimeObjects;
        }
        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static NoxUnityObjectMeta GetMeta(long MID)
        {
            return MetaStore[MID];
        }

        [Obsolete("Dont use outside NoxFiretail namespace")]
        public static void DropAllReferences(NoxUnityObject obj)
        {
            InverseObjectStore.Remove(obj.Puppet);
            ActiveDataRef.ObjectStore.Remove(obj.IUID);
        }
        #endregion

        #region NoxObject related classes
        private class SerializableData
        {

            public long VersionID;
            /// <summary>
            /// Store stuff that has nothing to do with Unity
            /// </summary>
            public Dictionary<long, NoxObject> CustomStore = new Dictionary<long, NoxObject>();
            /// <summary>
            /// Contains all ever existed objects ever created in-game. <br />
            /// Does not have upper limit, but will still crash when lifetime number of objects reaches something around 1e+500 <br />
            /// In reality, that was the last number of objects I saw before it caused crash on the phone. But I assume this could even be memory :P
            /// </summary>
            /// <typeparam name="String"></typeparam>
            /// <typeparam name="NoxObjectInstance"></typeparam>
            /// <returns></returns>
            public Dictionary<String, NoxUnityObject> ObjectStore = new Dictionary<String, NoxUnityObject>();
            /// <summary>
            /// Total number of objects ever created. Theoretically, limit is somewhere around 1e+500, but it's unrealistic.
            /// </summary>
            public SerializableBigInt LifeTimeObjects = 0;

            public SerializableData() { }

            public String SerializeData()
            {
                foreach (NoxUnityObject noi in ObjectStore.Values)
                {
                    noi.LatestPosition = new SerializableVector3(noi.Puppet.transform.position);
                    noi.LatestRotation = new SerializableQuaternion(noi.Puppet.transform.rotation);
                    // !!!!!!!!!!!
                    // Do not change to Newtonsoft.JSON.OnSerialize Attribute
                    // !!!!!!!!!!!
                    noi.Serialize();
                }
                return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }

            public void PostDeserialzation()
            {
                foreach (NoxUnityObject noi in ObjectStore.Values)
                {
                    noi.Puppet = Instantiate(Resources.Load<GameObject>("NoxFiretail/SerialPrefabs/" + MetaStore[noi.MID].PrefabName));
                    noi.Puppet.transform.position = noi.LatestPosition.ConvertToUnityVector3();
                    noi.Puppet.transform.rotation = noi.LatestRotation.ConvertToUnityQuanterion();
                    noi.Puppet.transform.SetParent(GameObject.Find(MetaStore[noi.MID].DefaultParent).transform);
                    InverseObjectStore.Add(noi.Puppet, noi);
                    // !!!!!!!!!!!
                    // Do not change to Newtonsoft.JSON.OnDeserialize Attribute
                    // !!!!!!!!!!!
                    noi.Deserialize();
                }
            }
        }

        [Serializable]
        public class NoxUnityObjectMeta
        {
            /// <summary>
            /// Use this to set ID aka MetaStringID
            /// </summary>
            public String MetaStringID;
            /// <summary>
            /// If left None, it will put in root <br />
            /// Must be an absolute path
            /// </summary>
            public String DefaultParent;
            /// <summary>
            /// Default path in Resource/NoxFiretail/SerialPrefabs/
            /// </summary>
            public String PrefabName;

        }
        #endregion

    }
}
#pragma warning restore CS0612 // Type or member is obsolete