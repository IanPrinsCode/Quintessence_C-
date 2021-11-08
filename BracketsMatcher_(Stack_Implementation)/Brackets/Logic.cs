using System;
using System.Collections.Generic;
using System.Linq;

namespace Brackets
{
    class Logic
    {
        List<char> bracketsList;
        List<char> stack;

        static List<char> open = new List<char>() { '(', '[', '{', '<' };
        static Dictionary<char, char> matchingMap = new Dictionary<char, char>()
        {
            { '(',')' },
            { ')','(' },
            { '[',']' },
            { ']','[' },
            { '{','}' },
            { '}','{' },
            { '<','>' },
            { '>','<' }
        };

        public Logic(List<char> brackets)
        {
            bracketsList = brackets;
            stack = new List<char>();
        }

        public static Boolean IsOpen(char bracket)
        {
            return open.Contains(bracket);
        }

        public void DoSort()
        {
            List<char> temp = new List<char>();

            foreach (char bracket in bracketsList)
            {
                if (IsOpen(bracket))
                    temp.Insert(0, bracket);
                else
                    temp.Add(bracket);
            }
            bracketsList = temp;
        }

        public string GetMatchingMessage()
        {
            DoSort();

            foreach (char bracket in bracketsList)
            {
                char matchingBracket = matchingMap[bracket];

                if (IsOpen(bracket))
                    stack.Add(bracket);
                else if (stack.Contains(matchingBracket))
                    stack.Remove(matchingBracket);
                else
                    return "'" + bracket + "' has no matching bracket.";
            }
            if (stack.Count() == 0)
                return "The string is correct! There are no mismatched brackets.";
            else
                return "'" + stack[0] + "' has no matching bracket.";
        }
    }
}
