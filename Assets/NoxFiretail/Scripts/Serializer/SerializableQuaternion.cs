using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoxFiretail.Scripts.Serializer
{
    public struct SerializableQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public SerializableQuaternion(Quaternion quat)
        {
            x = quat.x;
            z = quat.z;
            y = quat.y;
            w = quat.w;
        }

        public Quaternion ConvertToUnityQuanterion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}
