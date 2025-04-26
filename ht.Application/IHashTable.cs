namespace ht.Application
{
    public interface IHashTable<K, V>
    {
        int Collisions {  get; }
        void Insert(K key, V value);
        V Search(K key);
    }

}
