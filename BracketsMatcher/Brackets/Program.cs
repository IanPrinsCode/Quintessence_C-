using System;
using System.Collections.Generic;

namespace Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> input;
            Logic bracketsReader;

            Console.Write("Please enter a string of brackets: ");

            input = Test.GetInput();
            bracketsReader = new Logic(input);
            bracketsReader.MatchBrackets();
        }
    }
}
