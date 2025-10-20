using DStruct;
using System.Diagnostics;
using System.Globalization;
using DStruct.BinaryTrees;
using AaUS2.structures;

namespace AaUS2.tests
{
    public class TreeTest
    {
        public static void TestInsertRemoveFind(string csvFile, string csvFileTotal)
        {
            int insertCount = 10_000_000;
            int removeCount = 2_000_000;
            int findCount = 5_000_000;
            const double findMinMaxProbability = 0.5;
            var rand = new Random(50);

            double bstTotalInsertMs = 0.0, avlTotalInsertMs = 0.0, rbTotalInsertMs = 0.0;
            double bstTotalRemoveMs = 0.0, avlTotalRemoveMs = 0.0, rbTotalRemoveMs = 0.0;
            double bstTotalFindMs = 0.0, avlTotalFindMs = 0.0, rbTotalFindMs = 0.0;
            double bstTotalMinMaxMs = 0.0, avlTotalMinMaxMs = 0.0;

            var bst = new AaUS2.structures.BinarySearchTree<int>();
            var avl = new AaUS2.structures.AVLTree<int>();
            var rb = new RedBlackTree<int>();
            var values = new List<int>(insertCount);

            for (int i = 0; i < insertCount; i++) values.Add(i);
            for (int i = insertCount - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (values[i], values[j]) = (values[j], values[i]);
            }

            using var writer = new StreamWriter(csvFile, append: false);
            using var writer2 = new StreamWriter(csvFileTotal, append: true);
            var sw = new Stopwatch();

            // insert
            Console.WriteLine("Starting insert test...");
            for (int i = 0; i < insertCount; i++)
            {
                int val = values[i];

                // BST
                sw.Restart();
                bst.Insert(val);
                sw.Stop();
                double bstMs = sw.Elapsed.TotalMilliseconds;
                bstTotalInsertMs += bstMs;
                writer.WriteLine($"Insert,BST,{i},{bstMs:F6}");

                if (rand.NextDouble() < findMinMaxProbability && bst.Size > 0)
                {
                    sw.Restart();
                    if (rand.NextDouble() < 0.5) bst.FindMin();
                    else bst.FindMax();
                    sw.Stop();
                    double minMaxMs = sw.Elapsed.TotalMilliseconds;
                    bstTotalMinMaxMs += minMaxMs;
                    writer.WriteLine($"MinMax,BST,{i},{minMaxMs:F6}");
                }

                // AVL
                sw.Restart();
                avl.Insert(val);
                sw.Stop();
                double avlMs = sw.Elapsed.TotalMilliseconds;
                avlTotalInsertMs += avlMs;
                writer.WriteLine($"Insert,AVL,{i},{avlMs:F6}");

                if (rand.NextDouble() < findMinMaxProbability && avl.Size > 0)
                {
                    sw.Restart();
                    if (rand.NextDouble() < 0.5) avl.FindMin();
                    else avl.FindMax();
                    sw.Stop();
                    double minMaxMs = sw.Elapsed.TotalMilliseconds;
                    avlTotalMinMaxMs += minMaxMs;
                    writer.WriteLine($"MinMax,AVL,{i},{minMaxMs:F6}");
                }

                // RB
                sw.Restart();
                rb.Insert(val);
                sw.Stop();
                double rbMs = sw.Elapsed.TotalMilliseconds;
                rbTotalInsertMs += rbMs;
                writer.WriteLine($"Insert,RB,{i},{rbMs:F6}");
            }
            Console.WriteLine("Insert phase done.");

            // remove
            Console.WriteLine("Starting remove test...");
            for (int i = 0; i < removeCount; i++)
            {
                int val = values[i];

                // BST
                sw.Restart();
                bst.Remove(val);
                sw.Stop();
                double bstMs = sw.Elapsed.TotalMilliseconds;
                bstTotalRemoveMs += bstMs;
                writer.WriteLine($"Remove,BST,{i},{bstMs:F6}");

                if (rand.NextDouble() < findMinMaxProbability && bst.Size > 0)
                {
                    sw.Restart();
                    if (rand.NextDouble() < 0.5) bst.FindMin();
                    else bst.FindMax();
                    sw.Stop();
                    double minMaxMs = sw.Elapsed.TotalMilliseconds;
                    bstTotalMinMaxMs += minMaxMs;
                    writer.WriteLine($"MinMax,BST,{i},{minMaxMs:F6}");
                }

                // AVL
                sw.Restart();
                avl.Remove(val);
                sw.Stop();
                double avlMs = sw.Elapsed.TotalMilliseconds;
                avlTotalRemoveMs += avlMs;
                writer.WriteLine($"Remove,AVL,{i},{avlMs:F6}");

                if (rand.NextDouble() < findMinMaxProbability && avl.Size > 0)
                {
                    sw.Restart();
                    if (rand.NextDouble() < 0.5) avl.FindMin();
                    else avl.FindMax();
                    sw.Stop();
                    double minMaxMs = sw.Elapsed.TotalMilliseconds;
                    avlTotalMinMaxMs += minMaxMs;
                    writer.WriteLine($"MinMax,AVL,{i},{minMaxMs:F6}");
                }

                // RB
                sw.Restart();
                rb.Remove(val);
                sw.Stop();
                double rbMs = sw.Elapsed.TotalMilliseconds;
                rbTotalRemoveMs += rbMs;
                writer.WriteLine($"Remove,RB,{i},{rbMs:F6}");
            }
            values.RemoveRange(0, removeCount);
            Console.WriteLine("Remove phase done.");

            // find
            Console.WriteLine("Starting find test...");
            int foundBST = 0, foundAVL = 0, foundRB = 0;
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

                sw.Restart();
                bool rbFound = rb.Find(val);
                sw.Stop();
                double rbMs = sw.Elapsed.TotalMilliseconds;
                rbTotalFindMs += rbMs;
                writer.WriteLine($"Find,RB,{i},{rbMs:F6}");
                if (rbFound) foundRB++;
            }

            Console.WriteLine($"Find phase done. Found {foundBST} (BST), {foundAVL} (AVL), {foundRB} (RB).");

            writer2.WriteLine($"Insert,BST,{bstTotalInsertMs:F6}");
            writer2.WriteLine($"Insert,AVL,{avlTotalInsertMs:F6}");
            writer2.WriteLine($"Insert,RB,{rbTotalInsertMs:F6}");
            writer2.WriteLine($"Remove,BST,{bstTotalRemoveMs:F6}");
            writer2.WriteLine($"Remove,AVL,{avlTotalRemoveMs:F6}");
            writer2.WriteLine($"Remove,RB,{rbTotalRemoveMs:F6}");
            writer2.WriteLine($"Find,BST,{bstTotalFindMs:F6}");
            writer2.WriteLine($"Find,AVL,{avlTotalFindMs:F6}");
            writer2.WriteLine($"Find,RB,{rbTotalFindMs:F6}");
            writer2.WriteLine($"MinMax,BST,{bstTotalMinMaxMs:F6}");
            writer2.WriteLine($"MinMax,AVL,{avlTotalMinMaxMs:F6}");

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

        private static bool TryFind(AaUS2.structures.BinarySearchTree<int> bst, int value)
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

        private static bool TryFind(AaUS2.structures.AVLTree<int> avl, int value)
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
