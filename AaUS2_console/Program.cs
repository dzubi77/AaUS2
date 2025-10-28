using AaUS2.tests;

namespace AaUS2_console 
{
    class MainClass
    {
        static void Main(String[] args)
        {
            /*
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("---------------------- Iteration no." + (i + 1) + " ----------------------");
                TreeTest.TestInsertRemoveFind("TreeTest" + (i + 1) + ".csv", "TotalStats" + (i + 1) + ".csv");
            }
            */
            TreeTest.TestRandomOperations(100000);
        }
    }
}