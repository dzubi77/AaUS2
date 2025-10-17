using System.Diagnostics;
using AaUS2.structures;

namespace AaUS2.tests
{
    public class TestBST 
    {
        public List<int> HlpList { get; } = new List<int>();

        public void TestRandomOperations(int count)
        {
             BinarySearchTree<int> tree = new BinarySearchTree<int>();
             Random rnd = new Random(10);

             for (int i = 0; i < count; ++i)
             {
                 var operationIndex = rnd.Next(100);
                 if (operationIndex < 60)
                 {
                    //insert
                    var value = rnd.Next(10000000);
                    tree.Insert(value);
                    HlpList.Add(value);
                 }
                 else if (operationIndex < 90)
                 {
                    //remove
                 }
                 else
                 {
                    //find
                 }
             }
        }
    }
}
