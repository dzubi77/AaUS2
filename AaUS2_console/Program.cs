using AaUS2;
using System.Diagnostics;

namespace AaUS2_console 
{
    class MainClass
    {
        static void Main(String[] args)
        {
            var tree = new BinarySearchTree<int>();
            Random r = new Random(42);
            int count = 10000000;

            var keys = Enumerable.Range(0, count).ToArray();
            for (int i = count - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                (keys[i], keys[j]) = (keys[j], keys[i]);
            }

            Console.WriteLine("Shuffled.");
            var sw = Stopwatch.StartNew();
            foreach (int k in keys)
            {
                tree.Insert(k);
            }
            sw.Stop();
            Console.WriteLine("BST::Insert time elapsed: " + sw.Elapsed);
        }
    }
}