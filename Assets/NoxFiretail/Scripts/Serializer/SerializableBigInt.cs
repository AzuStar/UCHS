using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NoxFiretail.Scripts.Serializer
{
    public struct SerializableBigInt
    {
        [JsonProperty]
        private String _Value;
        [JsonIgnore]
        public BigInteger Value;

        public SerializableBigInt(BigInteger big)
        {
            Value = big;
            _Value = big.ToString();
        }

        [OnDeserialized]
        private void Deserialize(StreamingContext sc)
        {
            Value = BigInteger.Parse(_Value);
        }

        public static implicit operator SerializableBigInt(BigInteger i)
        {
            return new SerializableBigInt(i);
        }
        public static implicit operator SerializableBigInt(int i)
        {
            return new SerializableBigInt(i);
        }
        public static SerializableBigInt operator +(SerializableBigInt sb, int i)
        {
            return new SerializableBigInt(sb.Value + i);
        }
        public static SerializableBigInt operator ++(SerializableBigInt sb)
        {
            return sb + 1;
        }
        public static implicit operator BigInteger(SerializableBigInt sb)
        {
            return sb.Value;
        }
        public static implicit operator string(SerializableBigInt bi)
        {
            return bi.ToString();
        }
        public override string ToString()
        {
            return _Value;
        }
    }
}
