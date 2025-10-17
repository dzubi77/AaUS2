using AaUS2;
using System.Diagnostics;
using AaUS2.structures;

namespace AaUS2_console 
{
    class MainClass
    {
        static void Main(String[] args)
        {
            var tree = new AVLTree<int>();
            for (int i = 0; i < 10; ++i)
            {
                tree.Insert(i + 1);
            }

            tree.Remove(9);
            tree.Remove(10);

            tree.ProcessLevelOrder(tree.Root, (n) => Console.Write(n.Data + " "));
        }
    }
}