using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoxFiretail.Scripts.Core;
using UnityEngine;

namespace NoxFiretail.Scripts.Serializer
{
#pragma warning disable CS0618 // Type or member is obsolete
    public static class UnityGameObjectExtensions
    {
        public static NoxUnityObject GetNoxObject(this GameObject @object)
        {
            return GameManager.GetNoxObject(@object);
        }
        /// <summary>
        /// This will destroy GameObject and attached NoxUnityObject Data
        /// </summary>
        /// <param name="object"></param>
        public static void NoxDestroy(this GameObject @object)
        {
            NoxUnityObject toBeRemoved = @object.GetNoxObject();
            if (toBeRemoved != null)
                toBeRemoved.Destroy();
        }

    }
#pragma warning restore CS0618 // Type or member is obsolete
}
