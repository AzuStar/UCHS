

namespace NoxFiretail.Scripts.Primitives
{
    /// <summary>
    /// Permanent indexer for meta and such.
    /// JSON serialization ready.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Indexer<T>
    {
        private BiDictionary<int, T> Index = new BiDictionary<int, T>();

        public Indexer() { }

        public void Add(T obj)
        {
            Index.Add(Index.Count, obj);
        }
        public T Get(int id)
        {
            return Index.GetValue(id);
        }
        public int GetID(T obj)
        {
            return Index.GetKey(obj);
        }
    }
}