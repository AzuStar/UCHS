using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoxFiretail.Scripts.Serializer
{

    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vect)
        {
            x = vect.x;
            z = vect.z;
            y = vect.y;
        }

        public Vector3 ConvertToUnityVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
