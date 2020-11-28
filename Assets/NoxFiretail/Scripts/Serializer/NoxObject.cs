using NoxFiretail.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using static NoxFiretail.Scripts.Core.GameCommon;
#pragma warning disable CS0618 // Type or member is obsolete
namespace NoxFiretail.Scripts.Serializer
{
    public abstract class NoxObject
    {
        public long ID;

        public NoxObject(string stringStore) : this(API.FiveCC(stringStore))
        {
        }
        public NoxObject(long ID)
        {
            GameManager.AddObject(ID, this);
        }
        public static NoxObject GetObject(long ID)
        {
            return GameManager.GetObject(ID);
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
