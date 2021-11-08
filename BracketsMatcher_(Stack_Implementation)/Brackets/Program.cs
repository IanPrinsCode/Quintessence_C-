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

            input = Test.GetInput();

            bracketsReader = new Logic(input);
            Console.WriteLine(bracketsReader.GetMatchingMessage());
        }
    }
}
