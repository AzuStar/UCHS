using System.Collections.Generic;

namespace NoxFiretail.Scripts.Primitives
{
    /// <summary>
    /// Use bidictionary to have key be dereferencable by value and vice versa.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class BiDictionary<T, V>
    {

        public Dictionary<T, V> Dict = new Dictionary<T, V>();
        public Dictionary<V, T> InverseDict = new Dictionary<V, T>();

        public BiDictionary() { }

        public int Count { get => Dict.Count; }

        public void Add(T key, V value)
        {
            Dict.Add(key, value);
            InverseDict.Add(value, key);
        }

        public bool ContainsKey(T key)
        {
            return Dict.ContainsKey(key);
        }
        public bool ContainsValue(V value)
        {
            return InverseDict.ContainsKey(value);
        }
        public T GetKey(V value)
        {
            return InverseDict[value];
        }
        public V GetValue(T key)
        {
            return Dict[key];
        }
        /// <summary>
        /// Removes KeyValuePair of specified value and returns removed key
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T RemoveKey(V value)
        {
            T t = InverseDict[value];
            InverseDict.Remove(value);
            Dict.Remove(t);
            return t;
        }
        /// <summary>
        /// Removes KeybaluePair of specified key and returns removed value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public V RemoveValue(T key)
        {
            V v = Dict[key];
            Dict.Remove(key);
            InverseDict.Remove(v);
            return v;
        }

    }
}