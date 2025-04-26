namespace ht.Application
{
    public class DoubleHashingHashTable<K, V> : IHashTable<K, V>
    {
        private int _size;
        private K[] keys;
        private V[] values;
        public int Collisions { get; private set; } = 0;

        public DoubleHashingHashTable(int size)
        {
            _size = size;
            keys = new K[_size];
            values = new V[_size];
        }

        private int Hash1(K key) => Math.Abs(key.GetHashCode()) % _size;
        private int Hash2(K key)
        {
            int step = 7 - (Math.Abs(key.GetHashCode()) % 7); // Secondary hash
            return step == 0 ? 1 : step; // Avoid zero step size
        }

        public void Insert(K key, V value)
        {
            int index = Hash1(key);
            int stepSize = Hash2(key);
            int attempts = 0;

            while (keys[index] != null && !keys[index].Equals(default(K)))
            {
                Collisions++;

                // Prevent infinite loop
                if (attempts >= _size)
                    throw new InvalidOperationException("HashTable is full!");
                else if (attempts >= _size * 0.75)
                    Resize();

                index = (index + stepSize) % _size;
                attempts++;
            }

            keys[index] = key;
            values[index] = value;
        }

        public V Search(K key)
        {
            int index = Hash1(key);
            int stepSize = Hash2(key);
            int attempts = 0;

            while (keys[index] != null)
            {
                if (keys[index].Equals(key))
                    return values[index];

                // Prevent infinite loop
                if (attempts >= _size)
                    return default(V); // Key not found

                index = (index + stepSize) % _size;
                attempts++;
            }
            return default(V);
        }

        private void Resize()
        {
            int newSize = _size * 2;
            K[] newKeys = new K[newSize];
            V[] newValues = new V[newSize];

            for (int i = 0; i < _size; i++)
            {
                if (keys[i] != null)
                {
                    int newIndex = Math.Abs(keys[i].GetHashCode()) % newSize;
                    newKeys[newIndex] = keys[i];
                    newValues[newIndex] = values[i];
                }
            }

            keys = newKeys;
            values = newValues;
            _size = newSize;
        }
    }


}
