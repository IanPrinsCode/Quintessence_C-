using System;
using System.Collections.Generic;
using System.Linq;

namespace Brackets
{
    class Logic
    {
        List<char> bracketsLeft;
        List<char> notMatched;

        static List<char> open = new List<char>() { '(', '[', '{', '<' };
        static List<char> closed = new List<char>() { ')', ']', '}', '>' };
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
            this.bracketsLeft = brackets;
            this.notMatched = new List<char>();
        }

        public static Boolean IsExactMatch(char bracketOne, char bracketTwo)
        {
            return matchingMap[bracketOne] == bracketTwo;
        }

        public static Boolean IsMatch(char bracketOne, char bracketTwo)
        {
            return ((IsOpen(bracketOne) && IsClosed(bracketTwo))
                || (IsOpen(bracketTwo) && IsClosed(bracketOne)));
        }

        public static Boolean IsOpen(char bracket)
        {
            return open.Contains(bracket);
        }

        public static Boolean IsClosed(char bracket)
        {
            return closed.Contains(bracket);
        }

        public void ExecuteMatch(int index)
        {
            int lastIndex = bracketsLeft.Count() - 1;

            bracketsLeft.RemoveAt(lastIndex);
            bracketsLeft.RemoveAt(index);
        }

        public void ExecuteNoMatch()
        {
            int lastIndex = bracketsLeft.Count() - 1;

            notMatched.Add(bracketsLeft[lastIndex]);
            bracketsLeft.RemoveAt(lastIndex);
        }

        public void DoErrorLastIndexMatching()
        {
            char current = notMatched[notMatched.Count() - 1];
            for (int i = 0; i < (notMatched.Count() - 1); i++)
            {
                if (IsMatch(current, notMatched[i]))
                {
                    Console.WriteLine("error: '" + current + "' does not match with '" + notMatched[i] + "'.");
                    notMatched.RemoveAt(notMatched.Count() - 1);
                    notMatched.RemoveAt(i);
                    return;
                }
            }
            Console.WriteLine("error: '" + current + "' does not have a corresponding bracket.");
            notMatched.RemoveAt(notMatched.Count() - 1);
        }

        public void DoLastIndexMatching()
        {
            char current = bracketsLeft[bracketsLeft.Count() - 1];
            for (int i = 0; i < (bracketsLeft.Count() - 1); i++)
            {
                if (IsExactMatch(current, bracketsLeft[i]))
                {
                    ExecuteMatch(i);
                    return;
                }
            }
            ExecuteNoMatch();
        }

        public void DoErrorOutput()
        {
            if (notMatched.Count() == 0)
                return;

            DoErrorLastIndexMatching();
            DoErrorOutput();
        }

        public void MatchBrackets()
        {
            if (bracketsLeft.Count() == 0)
            {
                if (notMatched.Count() == 0)
                    Console.WriteLine("The string is correct! There are no mismatched brackets.");
                else
                    DoErrorOutput();
                return;
            }

            DoLastIndexMatching();
            MatchBrackets();
        }
    }
}
