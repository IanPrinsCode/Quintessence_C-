using System;
using System.Collections.Generic;
using System.Linq;

namespace Brackets
{
    class Test
    {
        static List<char> brackets = new List<char>() { '(', '[', '{', '<', ')', ']', '}', '>' };

        public static List<char> GetInput()
        {
            string input;

            Console.Write("Please enter a string of brackets: ");
            input = Console.ReadLine().Trim();
            foreach (char bracket in input)
            {
                if (!brackets.Contains(bracket))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    return GetInput();
                }
            }
            return input.ToList();
        }
    }
}
