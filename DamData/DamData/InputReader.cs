using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace DamData
{
    class InputReader
    {
        public static Dam _currentDam;
        private static TimeSpan _executionTime;
        private DataRetriever _retriever;
        private Logger _logger;
        private string _input;
        private string _nameInput;
        private string _dateInput;
        List<string> _exitMessages = new List<string>()
        {
            "quit",
            "exit",
            "end"
        };

        public InputReader(DataRetriever retriever)
        {
            string logPath = @"C:\Users\27732\Desktop\Quintessence C# Projects\BonusProjects\DamData\DamData\LoggedData.txt";
            _retriever = retriever;
            _logger = new Logger(logPath);
        }

        public static void DoFreezeAndClear()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t[Press ENTER to continue]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
            Console.Clear();
        }

        private static string StandardizeDate(string date)
        {
            string[] dateArray;
            string month;

            if (date.Count(c => c == '-') == 2)
            {
                dateArray = date.Split('-', 3);
                month = dateArray[1];
                month = char.ToUpper(month[0]) + month.Substring(1);
                date = dateArray[0] + '-' + month + '-' + dateArray[2];
            }
            return date;
        }

        private bool IsValidCommand()
        {
            if (String.Equals(_input, "printalldams()", StringComparison.OrdinalIgnoreCase)
                && Program.IsTraditionalArray)
                return true;
            if (_input.ToLower().Contains("printdam"))
            {
                if (IsValidFormat() && IsValidParameters())
                    return true;
            }
            if (String.Equals(_input, "clearlog()", StringComparison.OrdinalIgnoreCase))
                return true;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tINVALID COMMAND\n");
            Console.ForegroundColor = ConsoleColor.White;
            DoFreezeAndClear();
            return false;
        }

        private bool IsValidFormat()
        {
            if (!(_input.Count(c => c == '(')==1) ||
                !(_input.Count(c => c == ')')==1) ||
                !(_input.Count(c => c == ',')==1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("# Invalid format! #\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }

        private bool IsValidParameters()
        {
            string temp = _input.Split('(', 2)[1].Split(')', 2)[0];
            _nameInput = temp.Split(',', 2)[0].ToUpper();
            _dateInput = StandardizeDate(temp.Split(',', 2)[1]);
            _currentDam = _retriever.GetCurrentDam(_nameInput);

            if (_currentDam == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("# Invalid {damName} parameter! #\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            if (!_currentDam.GetData().ContainsKey(_dateInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("# Invalid {date} parameter! #\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }

        private void ExecuteCommand()
        {
            if (String.Equals(_input, "printalldams()", StringComparison.OrdinalIgnoreCase))
            {
                _retriever.PrintAllDams(_logger);
            }
            else if (_input.ToLower().Contains("printdam"))
            {
                _retriever.PrintDamInfoForDate(_dateInput, _logger);
            }
            else if (String.Equals(_input, "clearlog()", StringComparison.OrdinalIgnoreCase))
                _logger.Clear();
        }

        private static void PrintCommands()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("POSSIBLE COMMANDS:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (Program.IsTraditionalArray)
                Console.WriteLine("printAllDams()");
            Console.WriteLine("printDam({damName},31-Jan-12)\n" +
                "clearLog()\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DoExitMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nGoodbye ..");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void LoopInput()
        {
            PrintCommands();
            Console.Write("Input -> ");
            _input = Console.ReadLine().Trim();
            Console.Clear();

            if (_exitMessages.Contains(_input))
            {
                DoExitMessage();
                return;
            }
            if (IsValidCommand())
            {
                //start logging execution time
                var sw = Stopwatch.StartNew();

                ExecuteCommand();

                //end logging, save result to a Timespan var and log result to file
                sw.Stop();
                _executionTime = sw.Elapsed;
                LogTimePerformance();
                sw.Reset();

                DoFreezeAndClear();
            }
            LoopInput();
        }

        private static void LogTimePerformance()
        {
            string performanceLogPath = @"C:\Users\27732\Desktop\Quintessence C# Projects\BonusProjects\DamData\DamData\PerformanceLogs.txt";
            Logger logger = new Logger(performanceLogPath);
            StreamWriter writer = logger.StartLog();

            writer.WriteLine("Execution time: " + _executionTime.ToString() + "\n");

            logger.EndLog();
        }
    }
}