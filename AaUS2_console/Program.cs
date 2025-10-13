using AaUS2;
using System.Diagnostics;

namespace AaUS2_console 
{
    class MainClass
    {
        static void Main(String[] args)
        {
            const int N = 10_000_000;
            int[] numbers = new int[N];

            for (int i = 0; i < N; i++)
                numbers[i] = i;

            Random rand = new Random(42);
            for (int i = N - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }

            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Console.WriteLine("Stopwatch starting");
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < N; ++i)
            {
                tree.Insert(numbers[i]);
            }
            sw.Stop();
            Console.WriteLine("BST::Insert - Time elapsed: " + sw.ElapsedMilliseconds + " ms");
        }
    }
}