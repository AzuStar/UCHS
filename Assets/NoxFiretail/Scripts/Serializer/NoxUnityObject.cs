using System.Numerics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NoxFiretail.Scripts.Core;

namespace NoxFiretail.Scripts.Serializer
{
#pragma warning disable CS0618 // Type or member is obsolete
    /// <summary>
    /// Use this to create Serializable objects. Always JsonIgnore any Unity Components.
    /// </summary>
    [Serializable]
    public abstract class NoxUnityObject
    {

        #region Puppet
        [JsonIgnore]
        public GameObject Puppet;

        [Obsolete]
        public SerializableVector3 LatestPosition;
        [Obsolete]
        public SerializableQuaternion LatestRotation;
        #endregion

        /// <summary>
        /// Instance Unique ID
        /// </summary>
        public readonly SerializableBigInt IUID;
        /// <summary>
        /// Meta ID
        /// </summary>
        public readonly long MID;

        public NoxUnityObject(string metaStringID) : this(GameCommon.IGC.FiveCC(metaStringID))
        {
        }
        public NoxUnityObject(long MID)
        {
            GameManager.NoxUnityObjectMeta meta = GameManager.GetMeta(MID);
            Puppet = GameObject.Instantiate(Resources.Load<GameObject>("NoxFiretail/SerialPrefabs/" + meta.PrefabName));
            if (!String.IsNullOrEmpty(meta.DefaultParent))
                Puppet.transform.SetParent(GameObject.Find(meta.DefaultParent).transform);
            this.IUID = GameManager.GetMyIUID(this);
            this.MID = MID;
        }

        #region Serialization Helpers
        /// <summary>
        /// This is called before object is serialized
        /// </summary>
        internal virtual void Serialize() { }
        /// <summary>
        /// This is called after object has been reconstructed (and everything is available)
        /// </summary>
        internal virtual void Deserialize() { }
        #endregion



        #region Functionality
        public void Destroy()
        {
            GameObject.Destroy(Puppet);
            GameManager.DropAllReferences(this);
        }
        public static NoxUnityObject GetObject(BigInteger UID)
        {
            return GameManager.GetNoxObject(UID);
        }
        public static NoxUnityObject GetObject(GameObject obj)
        {
            return GameManager.GetNoxObject(obj);
        }
        #endregion
        #region tricks
        public static implicit operator GameObject(NoxUnityObject nobj)
        {
            return nobj.Puppet;
        }
        public static implicit operator NoxUnityObject(GameObject gobj)
        {
            return GetObject(gobj);
        }
        #endregion
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
