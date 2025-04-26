namespace ht.Application
{
    public class ChainedHashTable<K, V> : IHashTable<K, V>
    {
        private readonly int _size;
        private List<KeyValuePair<K, V>>[] table;

        public int Collisions { get; private set; } = 0;

        public ChainedHashTable(int size)
        {
            _size = size;
            table = new List<KeyValuePair<K, V>>[_size];
            for (int i = 0; i < _size; i++)
                table[i] = new List<KeyValuePair<K, V>>();
        }

        private int Hash(K key) => Math.Abs(key.GetHashCode()) % _size;

        public void Insert(K key, V value)
        {
            int index = Hash(key);

            if (table[index].Count > 0)
                Collisions++;

            table[index].Add(new KeyValuePair<K, V>(key, value));
        }

        public V Search(K key)
        {
            int index = Hash(key);
            foreach (var pair in table[index])
                if (pair.Key.Equals(key)) return pair.Value;

            return default;
        }
    }


}
