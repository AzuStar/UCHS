using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// This part manages the stores and their backups
/// There is main data indexer
/// And slaves-backup stores

namespace NoxFiretail.Scripts.Core
{
    public partial class GameManager
    {
        private static StoreIndex MainDataIndex;

        #region Global Serialization handles
        /// <summary>
        /// Heavy method which handles corruption, backups and encryption
        /// </summary>
        private void MainDataIndexCall()
        {
            ActiveDataRef = new SerializableData();
        }
        public void SerializeLiveData(long versionID = 1, string path = "/test.sif")
        {
            ActiveDataRef.VersionID = versionID;
            File.WriteAllText(Application.persistentDataPath + path, ActiveDataRef.SerializeData());
        }

        /// <summary>
        /// Backups, versions, multifiles etc....
        /// </summary>
        private class StoreIndex
        {
            public int Firetail = 1;

            public string CurrentTarget;

            /// <summary>
            /// Backups
            /// </summary>
            public Dictionary<string, Store> Stores = new Dictionary<string, Store>();

            public class Store
            {
                int VersionID;
                string Path;
                /// <summary>
                /// For encryption
                /// </summary>
                string KeyCrypto;
            }
        }
        #endregion
    }
}
