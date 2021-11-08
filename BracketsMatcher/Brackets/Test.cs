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

            input = Console.ReadLine().Trim();
            foreach (char bracket in input)
            {
                if (!brackets.Contains(bracket))
                {
                    Console.Write("Invalid input. Please try again: ");
                    return GetInput();
                }
            }
            return input.ToList();
        }
    }
}
