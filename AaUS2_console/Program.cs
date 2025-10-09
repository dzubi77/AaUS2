using AaUS2;
using System.Diagnostics;

namespace AaUS2_console 
{
    class MainClass
    {
        static void Main(String[] args)
        {
            var tree = new BinarySearchTree<int>();

            Random r = new Random(DateTime.Now.Microsecond);
            int count = 10000000;
            var keys = Enumerable.Range(0, count).ToList();
            keys = keys.OrderBy(x => r.Next()).ToList();

            var sw = Stopwatch.StartNew();
            Console.WriteLine("Stopwatch started");

            for (int i = 0; i < count; ++i)
            {
                tree.Insert(keys[i]);
            }

            sw.Stop();
            Console.WriteLine("BST::Insert time elapsed: " + sw.Elapsed);
        }
    }
}