using ht.Application;
using System.Diagnostics;

int size = 100_000;
Random rand = new Random();
int[] dataset = new int[size];

for (int i = 0; i < dataset.Length; i++)
    dataset[i] = rand.Next(1, size * 2); // Random integers from 1 to (size * 2) to ensure randomness.

var chainedHashTable = new ChainedHashTable<int, int>(size);
var linearProbingTable = new LinearProbingHashTable<int, int>(size);
var doubleHashingTable = new DoubleHashingHashTable<int, int>(size);

MeasurePerformance("Chained Hash Table", chainedHashTable, dataset);
MeasurePerformance("Linear Probing", linearProbingTable, dataset);
MeasurePerformance("Double Hashing", doubleHashingTable, dataset);

static void MeasurePerformance<K, V>(string name, IHashTable<K, V> hashTable, K[] dataset)
{
    Stopwatch sw = new Stopwatch();

    Console.WriteLine($"Measuring {name} performance...");

    // Insert operations
    sw.Start();
    foreach (var key in dataset)
    {
        hashTable.Insert(key, (V)(object)key);
    }
    sw.Stop();
    Console.WriteLine($"Insertion Time: {sw.ElapsedMilliseconds} ms");

    // Search operations
    sw.Restart();
    foreach (var key in dataset)
        hashTable.Search(key);
    sw.Stop();
    Console.WriteLine($"Search Time: {sw.ElapsedMilliseconds} ms");

    Console.WriteLine($"Collisions During Insertions: {hashTable.Collisions}");
    Console.WriteLine("----------------------------------------------------");
}
