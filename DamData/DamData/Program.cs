using System;

namespace DamData
{
    class Program
    {
        public static bool IsTraditionalArray;

        static bool IsValid(string input)
        {
            if (string.Equals(input, "bst", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(input, "ta", StringComparison.OrdinalIgnoreCase))
            {
                IsTraditionalArray = string.Equals(input, "ta", StringComparison.OrdinalIgnoreCase) ? true : false;
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            string readPath = @"C:\Users\27732\Desktop\Quintessence C# Projects\BonusProjects\DamData\DamData\DamData.xlsx";
            var fileReader = new DataReader(readPath);
            string operationType = "";
            Dam[] damArray;
            Tree binarySearchTree;
            DataRetriever retriever;

            while (!IsValid(operationType))
            {
                Console.Write("Which data-retrieving system would you like to use ?\n" +
                    "TA (traditional array)\n" +
                    "BST (binary search tree)\n" +
                    "=> ");
                operationType = Console.ReadLine();
                Console.Clear();
            }

            if (String.Equals(operationType, "ta", StringComparison.OrdinalIgnoreCase))
            {
                damArray = fileReader.ReadToArray();
                retriever = new DataRetriever(damArray);
            }
            else
            {
                binarySearchTree = fileReader.ReadToBST();
                retriever = new DataRetriever(binarySearchTree);
            }

            var inputReader = new InputReader(retriever);
            inputReader.LoopInput();
        }
    }
}
