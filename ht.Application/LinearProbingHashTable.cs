namespace ht.Application
{
    public class LinearProbingHashTable<K, V> : IHashTable<K, V>
    {
        private readonly int _size;
        private K[] keys;
        private V[] values;
        public int Collisions { get; private set; } = 0;

        public LinearProbingHashTable(int size)
        {
            _size = size;
            keys = new K[_size];
            values = new V[_size];
        }

        private int Hash(K key) => Math.Abs(key.GetHashCode()) % _size;

        public void Insert(K key, V value)
        {
            int index = Hash(key);

            while (keys[index] != null && !keys[index].Equals(default(K))) // Avoid overwriting valid keys
            {
                Collisions++;
                index = (index + 1) % _size; // Move to next slot (circular)
            }

            keys[index] = key;
            values[index] = value;
        }

        public V Search(K key)
        {
            int index = Hash(key);

            while (keys[index] != null)
            {
                if (keys[index].Equals(key))
                    return values[index];

                index = (index + 1) % _size; // Continue probing
            }
            return default(V);
        }
    }


}
