using AaUS2.structures;
using System.Diagnostics;
using System.Globalization;

namespace AaUS2.tests
{
    public class TreeTest
    {
        public static void TestInsertRemoveFind(string csvFile, string csvFileTotal)
        {
            int insertCount = 10_000_000;
            int removeCount = 2_000_000;
            int findCount = 5_000_000;
            int intervalFindCount = 1_000_000;

            double bstTotalInsertMs = 0.0;
            double avlTotalInsertMs = 0.0;
            double bstTotalRemoveMs = 0.0;
            double avlTotalRemoveMs = 0.0;
            double bstTotalFindMs = 0.0;
            double avlTotalFindMs = 0.0;

            var bst = new BinarySearchTree<int>();
            var avl = new AVLTree<int>();
            var values = new List<int>(insertCount);
            var rand = new Random(50);
            var sw = new Stopwatch();

            for (int i = 0; i < insertCount; i++) values.Add(i);
            for (int i = insertCount - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (values[i], values[j]) = (values[j], values[i]);
            }

            using var writer = new StreamWriter(csvFile, append: false);
            using var writer2 = new StreamWriter(csvFileTotal, append: true);

            Console.WriteLine("Starting insert test...");
            for (int i = 0; i < insertCount; i++)
            {
                int val = values[i];

                sw.Restart();
                bst.Insert(val);
                sw.Stop();
                double bstMs = sw.Elapsed.TotalMilliseconds;
                bstTotalInsertMs += bstMs;
                writer.WriteLine($"Insert,BST,{i},{bstMs:F6}");

                sw.Restart();
                avl.Insert(val);
                sw.Stop();
                double avlMs = sw.Elapsed.TotalMilliseconds;
                avlTotalInsertMs += avlMs;
                writer.WriteLine($"Insert,AVL,{i},{avlMs:F6}");
            }
            Console.WriteLine("Insert phase done.");

            Console.WriteLine("Starting remove test...");
            for (int i = 0; i < removeCount; i++)
            {
                int val = values[i];

                sw.Restart();
                bst.Remove(val);
                sw.Stop();
                double bstMs = sw.Elapsed.TotalMilliseconds;
                bstTotalRemoveMs += bstMs;
                writer.WriteLine($"Remove,BST,{i},{bstMs:F6}");

                sw.Restart();
                avl.Remove(val);
                sw.Stop();
                double avlMs = sw.Elapsed.TotalMilliseconds;
                avlTotalRemoveMs += avlMs;
                writer.WriteLine($"Remove,AVL,{i},{avlMs:F6}");
            }
            values.RemoveRange(0, removeCount);
            Console.WriteLine("Remove phase done.");

            Console.WriteLine("Starting find test...");
            int foundBST = 0;
            int foundAVL = 0;
            for (int i = 0; i < findCount; i++)
            {
                int val = values[rand.Next(values.Count)];

                sw.Restart();
                bool bstFound = TryFind(bst, val);
                sw.Stop();
                double bstMs = sw.Elapsed.TotalMilliseconds;
                bstTotalFindMs += bstMs;
                writer.WriteLine($"Find,BST,{i},{bstMs:F6}");
                if (bstFound) foundBST++;

                sw.Restart();
                bool avlFound = TryFind(avl, val);
                sw.Stop();
                double avlMs = sw.Elapsed.TotalMilliseconds;
                avlTotalFindMs += avlMs;
                writer.WriteLine($"Find,AVL,{i},{avlMs:F6}");
                if (avlFound) foundAVL++;
            }
            Console.WriteLine($"Find phase done. Found {foundBST} (BST), {foundAVL} (AVL).");

            writer2.WriteLine($"Insert,BST,{bstTotalInsertMs:F6}");
            writer2.WriteLine($"Insert,AVL,{avlTotalInsertMs:F6}");
            writer2.WriteLine($"Remove,BST,{bstTotalRemoveMs:F6}");
            writer2.WriteLine($"Remove,AVL,{avlTotalRemoveMs:F6}");
            writer2.WriteLine($"Find,BST,{bstTotalFindMs:F6}");
            writer2.WriteLine($"Find,AVL,{avlTotalFindMs:F6}");

            // interval find
            /*
            int successfulIntervalsBST = 0;
            int successfulIntervalsAVL = 0;
            int minInterval = 1000;

            for (int i = 0; i < intervalFindCount; i++)
            {
                int min = rand.Next(0, Math.Max(1, bst.Size - minInterval - 1));
                int max = min + minInterval + rand.Next(5000);

                sw.Restart();
                var resultBST = bst.FindAll(min, max);
                sw.Stop();
                if (resultBST.Count >= 500) successfulIntervalsBST++;
                writer.WriteLine($"IntervalFind,BST,{i},{sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)},Count={resultBST.Count}");

                sw.Restart();
                var resultAVL = avl.FindAll(min, max);
                sw.Stop();
                if (resultAVL.Count >= 500) successfulIntervalsAVL++;
                writer.WriteLine($"IntervalFind,AVL,{i},{sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)},Count={resultAVL.Count}");
            }

            Console.WriteLine($"Interval find done. Successful (≥500 elements): BST={successfulIntervalsBST}, AVL={successfulIntervalsAVL}");
            */
        }

        private static bool TryFind(BinarySearchTree<int> bst, int value)
        {
            try
            {
                bst.Find(value); 
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private static bool TryFind(AVLTree<int> avl, int value)
        {
            try
            {
                avl.Find(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
